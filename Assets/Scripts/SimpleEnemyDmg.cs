using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyDmg : MonoBehaviour
{
    public GameObject enemy;

    // Daño que realiza el enemigo

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<PlayerController>().lighthp == 0)
            {
                col.gameObject.GetComponent<PlayerController>().prehp--;
            }
            else if (col.gameObject.GetComponent<PlayerController>().lighthp > 1)
            {
                col.gameObject.GetComponent<PlayerController>().isInmortal = true;
                col.gameObject.GetComponent<PlayerController>().prehp -= 2;
            }
            else if (col.gameObject.GetComponent<PlayerController>().lighthp > 0)
            {
                col.gameObject.GetComponent<PlayerController>().isInmortal = true;
                col.gameObject.GetComponent<PlayerController>().prehp--;
            }
            enemy.GetComponent<SimpleEnemy>().afterAttack = true;
        }
    }
}
