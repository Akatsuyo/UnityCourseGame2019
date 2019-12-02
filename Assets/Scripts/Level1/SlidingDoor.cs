using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public GameObject enemy;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy.GetComponent<Health>().Empty += OpenDoor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenDoor(object sender, EventArgs e) {
        animator.enabled = true;
    }
}
