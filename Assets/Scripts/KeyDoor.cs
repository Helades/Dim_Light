using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject go;
    [SerializeField] int keysToOpen = 1;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player && player.RemoveKey(keysToOpen))
        {
            Destroy(go);
        }
    }
}
