using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFog : MonoBehaviour
{
    private float timing;
    private bool active;
    public GameObject player;

    private void Start()
    {
        timing = 0f;
        active = false;
    }
    private void Update()
    {
        if (active)
        {
            timing += Time.deltaTime;

            if (timing > 0.33f)
            {
                player.GetComponent<PlayerController>().prehp--;
                timing = 0f;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            active = false;
            timing = 0f;
        }
    }
}
