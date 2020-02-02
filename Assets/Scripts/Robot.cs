using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

	public float maxAngle;
	public float magnetMoveSpeed;

	private bool right;

	public float moveSpeed, jumpForce, movementSmoothing;
	public float throwForce;
	public Transform groundCheckOrigin;
	public float groundCheckRadius;

	private Part held;

	public int player;

	public float theifCooldown;
	private float theifCooldownCurrent;

	private bool facingRight = false;
	private bool wasGrounded = false;

	private void Start() {
		right = player == 1;
		facingRight = !right;
		theifCooldownCurrent = theifCooldown;
	}

	void Update() {
		Animator anim = GetComponent<Animator>();

		theifCooldownCurrent -= Time.deltaTime;

		float magnetMove = right ? -magnetMoveSpeed * Time.deltaTime : magnetMoveSpeed * Time.deltaTime;
		Transform pivot = transform.GetChild(0);

		pivot.Rotate(new Vector3(0, 0, 1f), magnetMove*magnetMoveSpeed);

		float angle = pivot.rotation.eulerAngles.z;
		while (angle >= 180) angle -= 360;
		while (angle < -180) angle += 360;

		if (angle > maxAngle) right = true;
		else if (angle < -maxAngle) right = false;

		pivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		float x = Input.GetAxis("Horizontal" + player);
		float y = Input.GetAxis("Vertical" + player);
		bool jump = Input.GetButtonDown("Jump" + player);

		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		Vector2 velocity = rb.velocity;

		Vector2 targetVelocity = new Vector2(x * moveSpeed, rb.velocity.y);
		rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

		if (x != 0) {
			anim.SetBool("isRunning", true);
		} else {
			anim.SetBool("isRunning", false);
		}

		//if (x > 0 && !facingRight) {
		//	transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		//	facingRight = true;
		//} else if (x < 0 && facingRight) {
		//	transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		//	facingRight = false;
		//}

		if (Physics2D.OverlapCircle(groundCheckOrigin.position, groundCheckRadius, 1 << 8) != null) {
			if (!wasGrounded) {
				anim.SetBool("isJumping", false);
				anim.Play("Idle");
				Debug.Log("hello");
			}
			wasGrounded = true;

			if (jump) {
				rb.AddForce(new Vector2(0, jumpForce));
				anim.SetBool("isJumping", true);
			}
		} else {
			wasGrounded = false;
		}

		if (held != null && Input.GetAxis("Submit" + player) < 0.1f) {
			held.transform.parent = null;
			Rigidbody2D heldRb = held.gameObject.AddComponent<Rigidbody2D>();
			//held.gameObject.AddComponent<CircleCollider2D>();

			if (heldRb != null) {
				Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
				heldRb.AddForce(direction * (right ? throwForce : -throwForce));
			}

			held.thrower = player;
			held.gameObject.layer = 10;
			
			held = null;
		}

		if (transform.position.x < -Game.i.screenEdge || transform.position.x > Game.i.screenEdge || transform.position.y < Game.i.bottomEdge) {
			Vector2 pos = transform.position;
			pos.y = Game.i.topEdge;
			pos.x = Random.Range(-Game.i.screenEdge*0.8f, Game.i.screenEdge*0.8f);
			rb.velocity = Vector2.zero;

			transform.position = pos;
		}

		if (transform.position.y > 200 && transform.position.y > Game.i.topEdge && rb.velocity.y > 0) {
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}
	}

	private void OnCollisionStay2D(Collision2D collision) {
		Part part = collision.collider.GetComponent<Part>();

		if (part != null) {
			Collide(part);
		} else {
			Collide(collision.collider.GetComponentInParent<Part>());
		}
	}

	private void OnTriggerStay2D(Collider2D collider) {
		Part part = collider.GetComponent<Part>();

		if (part != null) {
			Collide(part);
		} else {
			Collide(collider.GetComponentInParent<Part>());
		}
	}

	private void Collide(Part part) {
		if (part != null && held == null && Input.GetAxis("Submit" + player) > 0.1f) {
			if (part.transform.parent != null) {
				if (theifCooldownCurrent < 0) {
					part.transform.parent.parent.GetComponent<Robot>().held = null;
					theifCooldownCurrent = 1;
				} else return;
			}
			part.transform.parent = transform.GetChild(0);
			part.transform.localPosition = new Vector2(0, 2);

			Destroy(part.GetComponent<Rigidbody2D>());
			//Destroy(part.GetComponent<CircleCollider2D>());
			held = part;
			held.gameObject.layer = 11;
		}
	}
}
