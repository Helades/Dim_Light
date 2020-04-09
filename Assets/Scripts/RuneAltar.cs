using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneAltar : MonoBehaviour
{
    public bool isOn;
    private bool usable;
    private float alphaVar;
    public Light spotlight, pointlight;
    public SpriteRenderer isUsable, isUsed;

    private void Start()
    {
        isOn = false;
        spotlight.enabled = false;
        pointlight.enabled = false;
        alphaVar = 0;
    }

    public void Update()
    {
        if (isOn && alphaVar > 0)
        {
            alphaVar -= 0.5f * Time.deltaTime;
            isUsed.color = new Color(1f, 1f, 1f, alphaVar);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<PlayerController>().runes > 0 && !isOn)
            {
                isUsable.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetButtonDown("Use") && !isOn && col.GetComponent<PlayerController>().RemoveRune())
        {
            isOn = true;
            spotlight.enabled = true;
            pointlight.enabled = true;
            alphaVar = 1f;
            isUsed.color = new Color(1f, 1f, 1f, alphaVar);
            isUsable.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        { 
        isUsable.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
