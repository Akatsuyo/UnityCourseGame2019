using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform following;
    public float smoothTime;
    public Vector3 followTransform;

    Vector3 vel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = following.TransformPoint(followTransform);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, smoothTime);
    }
}
