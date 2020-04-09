using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public GameObject mesh;

    public float speed;
    private float iniscale, endscale, offset;
    public bool opened;

    public enum StateIds
    {
        Vertical1,
        Vertical2,
        Horizontal1,
        Horizontal2,
        Rotation1,
        Rotation2
    }

    public StateIds estadoID;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        switch (estadoID)
        {
            case StateIds.Vertical1:

                if (opened && door.localScale.y > 0)
                {
                    door.localScale = new Vector3(door.localScale.x, door.localScale.y - speed * Time.deltaTime, door.localScale.z);
                }
                else if (!opened && door.localScale.y < 1)
                {
                    door.localScale = new Vector3(door.localScale.x, door.localScale.y + speed * Time.deltaTime, door.localScale.z);
                }
                else if (door.localScale.y > 1)
                {
                    door.localScale = new Vector3(door.localScale.x, 1, door.localScale.z);
                }

                break;

            case StateIds.Vertical2:

                break;

            case StateIds.Horizontal1:

                if (opened && door.localScale.y > 0)
                {
                    door.localScale = new Vector3(door.localScale.x - speed * Time.deltaTime, door.localScale.y, door.localScale.z);
                }
                else if (!opened && door.localScale.y < 1)
                {
                    door.localScale = new Vector3(door.localScale.x + speed * Time.deltaTime, door.localScale.y, door.localScale.z);
                }
                else if (door.localScale.y > 1)
                {
                    door.localScale = new Vector3(1, door.localScale.y, door.localScale.z);
                }

                break;

            case StateIds.Horizontal2:

                break;

            case StateIds.Rotation1:

                break;

            case StateIds.Rotation2:

                break;
        }
    }
}
