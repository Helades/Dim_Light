using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    // Plataforma Rompible

    private float timeout;
    private bool disabled, touching;
    private Transform self;
    private float scalex, scaley, scalez;

    void Start()
    {
        timeout = 0f;
        disabled = false;
        touching = false;
        self = GetComponent<Transform>();
        scalex = self.localScale.x;
        scaley = self.localScale.y;
        scalez = self.localScale.z;
    }

    void Update()
    {
        if (disabled)
        {
            timeout -= Time.deltaTime * 1f;

            if (timeout < -3f)
            {
                transform.localScale = new Vector3 (scalex, scaley, scalez);
                timeout = 0f;
                disabled = false;
            }
        }
        else if (touching)
        {
            timeout += Time.deltaTime * 1f;

            if (timeout > 1.5f) {

                transform.localScale = new Vector3(0f, 0f, 0f);
                timeout = 0f;
                disabled = true;
                touching = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touching = true;
        }
    }
}
