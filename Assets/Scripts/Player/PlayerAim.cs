 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Controlador del apuntado del jugador

    public Vector3 direction;
    public Transform ca;
    private float rotatevarx, rotatevary;

    private void Start()
    {
        rotatevarx = 0f;
        rotatevary = 0f;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenCentre = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f, 0);
        Vector3 result = Input.mousePosition - screenCentre;
        result.Normalize();
        gameObject.transform.forward = result;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
        direction = new Vector3(-mousePosition.x + gameObject.transform.position.x, -mousePosition.y + gameObject.transform.position.y, 0);

        if (direction.x < -4)
        {
            if (rotatevary > -6f)
            {
                rotatevary -= 12f * Time.deltaTime;
            }
        }
        else if (direction.x > 4)
        {
            if (rotatevary < 6f)
            {
                rotatevary += 12f * Time.deltaTime;
            }
        }
        else if (direction.x <= 4 && direction.x >=-4)
        {
            if (rotatevary < 0.25f && rotatevary > -0.25f)
            {
                rotatevary = 0f;
            }
            else if (rotatevary < 0f)
            {
                rotatevary += 6f * Time.deltaTime;
            }
            else if (rotatevary > 0f)
            {
                rotatevary -= 6f * Time.deltaTime;
            }
        }

        if (direction.y > 2)
        {
            if (rotatevarx > -5f)
            {
                rotatevarx -= 10f * Time.deltaTime;
            }

        }
        else if (direction.y < -2)
        {
            if (rotatevarx < 5f)
            {
                rotatevarx += 10f * Time.deltaTime;
            }
        }
        else if (direction.y <= 2 && direction.y >= -2)
        {
            if (rotatevarx < 0.25f && rotatevarx > -0.25f)
            {
                rotatevarx = 0f;
            }
            else if (rotatevarx < 0f)
            {
                rotatevarx += 5f * Time.deltaTime;
            }
            else if (rotatevarx > 0f)
            {
                rotatevarx -= 5f * Time.deltaTime;
            }
        }
        ca.rotation = Quaternion.Euler (rotatevarx, rotatevary, 0);
    }
}
