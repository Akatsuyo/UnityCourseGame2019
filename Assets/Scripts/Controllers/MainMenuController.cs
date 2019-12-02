using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Find saved game, and rename start game to continue if there is one.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        Global.GameController.StartNewGame();
    }

    public void OnLoad()
    {

    }

    public void OnSettings()
    {

    }

    public void OnExit()
    {
        Application.Quit();
    }
}
