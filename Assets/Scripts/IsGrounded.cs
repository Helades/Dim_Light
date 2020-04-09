using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "MobileFloor")
        {
            player.GetComponent<PlayerController>().isGrounded = true;

            if (col.gameObject.tag == "MobileFloor")
            {
                player.transform.parent = col.transform;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "MobileFloor")
        {
            player.GetComponent<PlayerController>().isGrounded = true;

            if (col.gameObject.tag == "MobileFloor")
            {
                player.transform.parent = col.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "MobileFloor")
        {
            player.GetComponent<PlayerController>().isGrounded = false;

            if (col.gameObject.tag == "MobileFloor")
            {
                player.transform.parent = null;
            }
        }
    }
}
