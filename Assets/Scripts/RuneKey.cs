using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuneKey : MonoBehaviour
{
    public char type;
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        { 
            if (type == 'k')
            {
                col.GetComponent<PlayerController>().key++;
                Destroy(gameObject);
            }
            else if (type == 'r')
            {
                col.GetComponent<PlayerController>().runes++;
                Destroy(gameObject);
            }
            else if (type == 'w')
            {
                gm.won = true;
                Destroy(gameObject);
            }
        }
    }
}
