using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobilePlatform : MonoBehaviour
{
    public bool active;
    private Animator animator;
    public string playAnimation;

    MobilePlatform()
    {
        active = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnActivation()
    {
        if (!active)
        {
            active = true;
            animator.SetBool("active", true);
        }
        else
        {
            active = false;
            animator.SetBool("active", false);
        }
            
    }
}
