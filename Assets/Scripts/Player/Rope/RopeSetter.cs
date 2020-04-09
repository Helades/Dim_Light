using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    public Rope rope;
    private float counter;

    public RopeSetter()
    {
        counter = 0;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && rope.line)
        {
            Destroy(rope.line);
            counter = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !rope.line)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenCentre = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f);
            Vector3 result = mousePos - screenCentre;
            rope.SetStart(result);
        }  
        else if (rope.line)
        {
            counter += Time.deltaTime;

            if (counter > 4f)
            {
                Destroy(rope.line);
                counter = 0;
            }
        }
    }
}
