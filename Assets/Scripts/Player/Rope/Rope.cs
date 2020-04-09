using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public LineRenderer line;
    public Rigidbody2D origin;
    private Vector3 velocity;
    public Material rope;

    public float line_width = 0.25f;
    public float speed = 65f;
    public float pullforce = 10f;

    private bool pull = false;

    public void SetStart (Vector3 targetPos)
    {
        line = GetComponent<LineRenderer>();
        if (!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.startWidth = line_width;
        line.endWidth = line_width;
        line.textureMode = LineTextureMode.Tile;
        line.material = rope;

        targetPos.Normalize();
        Vector2 direction = new Vector2 (targetPos.x, targetPos.y);
        //Debug.Log(direction);
        transform.position = new Vector2 (origin.position.x, origin.position.y) + direction;
        velocity = direction * speed;
        pull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (line)
        {
            if (pull)
            {
                Vector2 dir = (Vector2)transform.position - origin.position;
                origin.AddForce(dir * pullforce);
            }
            else
            {
                transform.position += velocity * Time.deltaTime;
            }


            float distance = Vector2.Distance(transform.position, origin.position);
            
            if (distance <= 20f)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, origin.position);
            }
            else
            {
                Destroy(line);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor" || col.tag == "Untagged")
        {
            velocity = Vector2.zero;
            pull = true;
        }
        
    }
}
