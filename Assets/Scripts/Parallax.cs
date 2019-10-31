using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallax;
    public float pre;

    float startPos;
    float spriteW;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        spriteW = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Camera.main.transform.position.x * parallax;

        transform.position = new Vector2(startPos + dist, transform.position.y);

        float temp = Camera.main.transform.position.x * (1 - parallax);
        if (temp + pre > startPos + spriteW) {
            startPos += spriteW;
        } else if (temp - pre < startPos - spriteW) {
            startPos -= spriteW;
        }
    }
}
