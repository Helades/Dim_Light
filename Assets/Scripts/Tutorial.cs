using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public UnityEvent isTriggered, isNotTriggered;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isTriggered.Invoke();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isTriggered.Invoke();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isNotTriggered.Invoke();
    }
}
