using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    // Movimiento del enemigo

    public Rigidbody2D rb;
    public Transform player, enemy;
    public GameObject goenemy;
    public Animator aniEnemy;

    private bool active;
    public bool afterAttack;
    public float playerDirection, patrolDirection, maxPatrol, minPatrol, chasespeed, patrolspeed, afterCounter;
    public int hp;
    private float originalScale;

    void Start()
    {
        active = false;
        patrolDirection = -1f;
        hp = 1;
        afterAttack = false;
        originalScale = goenemy.transform.localScale.x;
        aniEnemy.SetBool("Hit", false);
        aniEnemy.SetBool("Patrolling", true);
        aniEnemy.SetBool("Death", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            chasespeed = 0;
            patrolspeed = 0;
            goenemy.GetComponent<CapsuleCollider2D>().enabled = false;
            rb.gravityScale = 0;
            aniEnemy.SetBool("Hit", false);
            aniEnemy.SetBool("Patrolling", false);
            aniEnemy.SetBool("Death", true);
            rb.velocity = new Vector2(0, 0);
            //goenemy.SetActive(false);
        }
        else
        {
            //chasespeed = 750;
            //patrolspeed = 300;
            goenemy.GetComponent<CapsuleCollider2D>().enabled = true;
            rb.gravityScale = 1;

            if (active && player.position.x < enemy.transform.position.x)
            {
                playerDirection = -1f;
                goenemy.transform.localScale = new Vector3(-originalScale, goenemy.transform.localScale.y, goenemy.transform.localScale.z);
                aniEnemy.SetBool("Hit", false);
                aniEnemy.SetBool("Patrolling", false);
                aniEnemy.SetBool("Death", false);
            }
            else if (active && player.position.x > enemy.transform.position.x)
            {
                playerDirection = 1f;
                goenemy.transform.localScale = new Vector3(-originalScale, goenemy.transform.localScale.y, goenemy.transform.localScale.z);
                aniEnemy.SetBool("Hit", false);
                aniEnemy.SetBool("Patrolling", false);
                aniEnemy.SetBool("Death", false);
            }
            else if (!active && enemy.transform.position.x > maxPatrol)
            {
                patrolDirection = -1f;
                goenemy.transform.localScale = new Vector3(originalScale, goenemy.transform.localScale.y, goenemy.transform.localScale.z);
                aniEnemy.SetBool("Hit", false);
                aniEnemy.SetBool("Patrolling", true);
                aniEnemy.SetBool("Death", false);
            }
            else if (!active && enemy.transform.position.x < minPatrol)
            {
                patrolDirection = 1f;
                goenemy.transform.localScale = new Vector3(-originalScale, goenemy.transform.localScale.y, goenemy.transform.localScale.z);
                aniEnemy.SetBool("Hit", false);
                aniEnemy.SetBool("Patrolling", true);
                aniEnemy.SetBool("Death", false);
            }

            if (active)
            {
                if (afterAttack)
                {
                    aniEnemy.SetBool("Hit", true);
                    aniEnemy.SetBool("Patrolling", false);
                    aniEnemy.SetBool("Death", false);
                    rb.velocity = new Vector2(playerDirection * 50f * Time.deltaTime, rb.velocity.y);
                    afterCounter += Time.deltaTime;

                    if (afterCounter > 1.5f)
                    {
                        afterAttack = false;
                        afterCounter = 0f;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(playerDirection * chasespeed * Time.deltaTime, rb.velocity.y);
                }

            }
            else if (!active)
            {
                if (afterAttack)
                {
                    aniEnemy.SetBool("Hit", true);
                    aniEnemy.SetBool("Patrolling", true);
                    aniEnemy.SetBool("Death", false);
                    rb.velocity = new Vector2(playerDirection * 50f * Time.deltaTime, rb.velocity.y);
                    afterCounter += Time.deltaTime;

                    if (afterCounter > 1.5f)
                    {
                        afterAttack = false;
                        afterCounter = 0f;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(patrolDirection * patrolspeed * Time.deltaTime, rb.velocity.y);
                }
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
        }
    }
}
