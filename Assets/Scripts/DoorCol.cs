using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCol : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            door.GetComponent<Door>().opened = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            door.GetComponent<Door>().opened = false;
    }
}
