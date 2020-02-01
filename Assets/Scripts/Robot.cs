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

	private void Start() {
		GetComponent<MeshRenderer>().material.color = Game.i.colours[player];
		transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Game.i.colours[player];

		right = player == 1;
	}

	void Update() {
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

		if (jump && Physics2D.OverlapCircle(groundCheckOrigin.position, groundCheckRadius, 1 << 8) != null) {
			rb.AddForce(new Vector2(0, jumpForce));
		}

		if (held != null && Input.GetButtonUp("Submit" + player)) {
			held.transform.parent = null;
			Rigidbody2D heldRb = held.gameObject.AddComponent<Rigidbody2D>();
			held.gameObject.AddComponent<CircleCollider2D>();

			if (heldRb != null) {
				Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
				heldRb.AddForce(direction * (right ? throwForce : -throwForce));
			}

			held.thrower = player;
			
			held = null;
		}

		if (transform.position.x < -Game.i.screenEdge || transform.position.x > Game.i.screenEdge || transform.position.y < Game.i.bottomEdge) {
			Vector2 pos = transform.position;
			pos.y = Game.i.topEdge;
			pos.x = Random.Range(-Game.i.screenEdge*0.8f, Game.i.screenEdge*0.8f);

			transform.position = pos;
		}
	}

	private void OnCollisionStay2D(Collision2D collision) {
		Part part = collision.collider.GetComponent<Part>();

		if (part != null && held == null && Input.GetButton("Submit" + player)) {
			if (part.transform.parent != null) {
				part.transform.parent.parent.GetComponent<Robot>().held = null;
			}
			part.transform.parent = transform.GetChild(0);
			part.transform.localPosition = new Vector2(0, 2);

			Destroy(part.GetComponent<Rigidbody2D>());
			Destroy(part.GetComponent<CircleCollider2D>());
			held = part;
		} 
	}

	private void OnTriggerStay2D(Collider2D collider) {
		Part part = collider.GetComponent<Part>();

		if (part != null && held == null && Input.GetButton("Submit" + player)) {
			if (part.transform.parent != null) {
				part.transform.parent.parent.GetComponent<Robot>().held = null;
			}
			part.transform.parent = transform.GetChild(0);
			part.transform.localPosition = new Vector2(0, 2);

			Destroy(part.GetComponent<Rigidbody2D>());
			Destroy(part.GetComponent<CircleCollider2D>());
			held = part;
		}
	}
}
