using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Palanca : MonoBehaviour
{
    public SpriteRenderer usable, info;
    private float alphavar;
    private bool used;
    public GameObject vinculated;

    // Start is called before the first frame update
    void Start()
    {
        alphavar = 0;
        usable.color = new Color(1f, 1f, 1f, 0f);
        used = false;
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

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player" && !used)
            usable.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnTriggerStay2D (Collider2D col)
    {
        if (col.tag == "Player") {

            if (Input.GetButtonDown("Use") && !used && col.GetComponent<PlayerController>().lighthp > 0) {
                col.GetComponent<PlayerController>().prehp -= 1;
                usable.color = new Color(1f, 1f, 1f, 0f);
                alphavar = 1f;
                info.color = new Color(1f, 1f, 1f, alphavar);
                used = true;
                vinculated.GetComponent<MobilePlatform>().OnActivation();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && !used)
            usable.color = new Color(1f, 1f, 1f, 0f);
    }
}

