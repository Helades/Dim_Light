using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public Light light1, light2;

    private bool touching;

    void Start()
    {
        light1.intensity = 0;
        light2.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touching)
        {
            if (light1.intensity > 0)
                light1.intensity -= 25f * Time.deltaTime;

            if (light2.intensity > 0)
                light2.intensity -= 12.5f * Time.deltaTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            touching = true;         
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            touching = true;

            if (light1.intensity < 100)
                light1.intensity += 50f * Time.deltaTime;

            if (light1.intensity < 50)
                light2.intensity += 25f * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            touching = false;
    }
}
