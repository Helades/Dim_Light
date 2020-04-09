using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar2 : MonoBehaviour
{
    public SpriteRenderer usable, info;
    public GameManager gm;
    public TutorialGameManager tgm;

    private float alphavar;
    private bool notused;

    // Start is called before the first frame update
    void Start()
    {
        alphavar = 0;
        usable.color = new Color(1f, 1f, 1f, 0f);
        notused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alphavar > 0)
        {
            alphavar -= 0.5f * Time.deltaTime;
            info.color = new Color(1f, 1f, 1f, alphavar);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            usable.color = new Color(1f, 1f, 1f, 1f);
            notused = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetButtonDown("Use") && notused)
            {
                col.GetComponent<PlayerController>().prehp = 10;
                usable.color = new Color(1f, 1f, 1f, 0f);
                alphavar = 1f;
                info.color = new Color(1f, 1f, 1f, alphavar);
                notused = false;
                if (gm)
                    gm.respawnPoint = new Vector2(transform.position.x, transform.position.y);
                else if (tgm)
                    tgm.respawnPoint = new Vector2(transform.position.x, transform.position.y);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            usable.color = new Color(1f, 1f, 1f, 0f);
            notused = true;
        }
    }
}
