using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Rigidbody rb;
    Animator anim;
    float movementSpeed = 500f;
    Vector3 direction;
    bool jump;
    bool slide;
    CapsuleCollider caps;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        caps = transform.GetChild(0).GetComponent<CapsuleCollider>();

        GameObject.FindGameObjectWithTag("tileManager").GetComponent<TileManager>().playerTransform = gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && anim.GetCurrentAnimatorStateInfo(0).IsName("running"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && anim.GetCurrentAnimatorStateInfo(0).IsName("running"))
        {
            slide = true;
            caps.height = 1.4f;
        }
    }

    private void FixedUpdate()
    {
        direction.z = movementSpeed * Time.fixedDeltaTime;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("running") || anim.GetCurrentAnimatorStateInfo(0).IsName("leftStrafe") || anim.GetCurrentAnimatorStateInfo(0).IsName("rightStrafe"))
            direction.x = Input.GetAxis("Horizontal") * 15;
        rb.velocity = direction;

        if(Input.GetAxis("Horizontal") == 0)
        {
            direction.x = 0;
        }

        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            anim.ResetTrigger("running");
            anim.ResetTrigger("leftStrafe");
            anim.SetTrigger("rightStrafe");
        }
        else if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            anim.ResetTrigger("running");
            anim.ResetTrigger("rightStrafe");
            anim.SetTrigger("leftStrafe");
        }
        else
        {
            anim.ResetTrigger("rightStrafe");
            anim.ResetTrigger("leftStrafe");
            anim.SetTrigger("running");
        }


        if (jump)
        {
            rb.useGravity = false;
            StartCoroutine(gravityOn());
            anim.SetTrigger("frontFlip");
            jump = false;
        }else if (slide)
        {
            rb.useGravity = false;
            StartCoroutine(gravityOn());
            anim.SetTrigger("slide");
            slide = false;
        }
    }

    IEnumerator gravityOn()
    {
        yield return new WaitForSeconds(1);
        rb.useGravity = true;
        caps.height = 3.76f;
    }
}
