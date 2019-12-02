using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        Global.GameController.LevelFader.NoFade();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string GetNextLoadingText()
    {
        // StringBuilder loadingText = new StringBuilder("Loading ");
        
        // if (dots == 3)
        //     dots = 0;
        // dots++;

        // for (int i = 0; i < dots; i++)
        // {
        //     loadingText.Append('.');
        // }

        // return loadingText.ToString();
        return "";
    }
}
