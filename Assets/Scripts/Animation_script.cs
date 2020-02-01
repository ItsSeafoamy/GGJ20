using UnityEngine;

public class Animation_script : MonoBehaviour
{
    private Animator anim;
    private bool jumping = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Vertical") && !anim.GetBool("isJumping"))
        {
            anim.SetBool("isJumping", true);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

}
