using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public string type;
    public float speed, limin, limax;
    private float oneortwo;
    public Transform[] positions;

    // Start is called before the first frame update
    void Start()
    {
        oneortwo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "h")
        {
            transform.position = new Vector3(oneortwo * speed * Time.deltaTime + transform.position.x, transform.position.y, transform.position.z);

            if (transform.position.x < limin)
            {
                oneortwo = 1;
            }
            if (transform.position.x > limax)
            {
                oneortwo = -1;
            }
        }
        else if (type == "v")
        {
            transform.position = new Vector3(transform.position.x, oneortwo * speed * Time.deltaTime + transform.position.y, transform.position.z);

            if (transform.position.y < limin)
            {
                oneortwo = 1;
            }
            if (transform.position.y > limax)
            {
                oneortwo = -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerController>().prehp -= 2;
        }
    }
}
