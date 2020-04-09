using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public GameObject enemy;

    public void NotActive ()
    {
        enemy.SetActive(false);
    }
}
