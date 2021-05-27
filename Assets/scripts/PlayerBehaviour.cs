using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 HorF;
    private Vector3 VerF;
    private bool onWall;
    private bool onRail;

    void Start()
    {
        Debug.Log("testing commit");
        rb = transform.GetComponent<Rigidbody>();

        HorF = new Vector3(0.2f, 0.0f, 0.0f);
        VerF = new Vector3(0.0f, -0.2f, 0.0f);
    }

    void Update()
    {
        if (!onWall && !onRail)
        {
            rb.MovePosition(transform.position + HorF + VerF);
        }
        else if (!onRail)
        {
            Debug.Log(VerF);
            rb.MovePosition(transform.position + VerF);
        }
        else if (!onWall)
        {
            rb.MovePosition(transform.position + HorF);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (onWall && onRail)
            {
                HorF[0] = -1 * HorF[0];
                onWall = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (onWall && onRail)
            {
                VerF[1] = -1 * VerF[1];
                onRail = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag == "platform")
        {
            onWall = true;
        }
        else if (collider.tag == "rail")
        {
            onRail = true;
        }
    }
}
