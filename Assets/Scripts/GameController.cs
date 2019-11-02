using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform levelsGroup;

    Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = levelsGroup.Find("Level1").GetComponent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
        currentLevel.RespawnEnemies();
        currentLevel.RespawnPlayer();
    }
}
