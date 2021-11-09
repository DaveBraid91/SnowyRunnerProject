using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PickUpFish();
        }
    }

    private void PickUpFish()
    {
        //increment fish count
        //increment score
        //play sfx
        //trigger an animation
        anim.SetTrigger("Pickup");
        GameStats.Instance.CollectCollectable();
    }

    public void OnShowChunk()
    {
        anim.SetTrigger("Idle");
    }
}
