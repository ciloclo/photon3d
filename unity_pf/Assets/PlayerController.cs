using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            if (Input.GetKey("up"))
            {
                transform.position += transform.forward * 0.02f;
                animator.SetBool("walking", true);
            }
            else if (Input.GetKey("down"))
            {
                transform.position -= transform.forward * 0.02f;
                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("walking", false);
            }
            if (Input.GetKey("right"))
            {
                transform.Rotate(0, 1.0f, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(0, -1.0f, 0);
            }
        }
    }

}
