using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Level currentLevel;
    public Fading fading;

    // Start is called before the first frame update
    void Start()
    {
        fading = transform.Find("Fading").GetComponent<Fading>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateLevel()
    {
        currentLevel = GameObject.Find("Level").GetComponent<Level>();
    }

    public void ResetLevel()
    {
        currentLevel.RespawnEnemies();
        currentLevel.RespawnPlayer();
    }

    public void OnLevelFinished()
    {
        fading.FadeOut("TownLevel");
    }

    public void OnFadeOutFinished(string sceneToLoad) {
        LoadSceneWithLoadingScreen(sceneToLoad);
    }

    public void OnFadeInFinished() {
        // Return controls
    }
    
    void LoadSceneWithLoadingScreen(string sceneToLoad) {
        SceneManager.LoadScene(Constants.LOADING_SCENE);

        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    void OnSceneLoaded() {
        Debug.Log("Scene loaded.");
        fading.FadeIn();
        UpdateLevel();
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        yield return new WaitForSecondsRealtime(2);
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        while (!load.isDone) {
            yield return null;
        }

        OnSceneLoaded();
    }
}
