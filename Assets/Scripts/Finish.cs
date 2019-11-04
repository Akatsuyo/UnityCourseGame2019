using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    Level level;

    // Start is called before the first frame update
    void Start()
    {
        level = GetComponentInParent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == Constants.PLAYER_NAME) {
            level.OnFinished();
        }
    }
}
