using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public LevelFader LevelFader { get; private set; }

    public AchievementSystem AchievementSystem { get; private set; }

    List<string> levels = new List<string>();
    public int LevelState { get; set; } = 0;
    GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        LevelFader = transform.Find("LevelFader").GetComponent<LevelFader>();
        AchievementSystem = transform.Find("AchievementSystem").GetComponent<AchievementSystem>();

        levels.Add("TutorialLevel");
        levels.Add("Level1");

        DontDestroyOnLoad(gameObject);

        // TODO: Try create a state when debugging, destroy self when not in main menu
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        currentState = GameState.CreateNewGameState();
        LoadLevel(true, true);
    }

    void LoadGame(string saveFilePath)
    {
        throw new NotImplementedException();
    }

    void SaveGame(string filePath)
    {
        throw new NotImplementedException();
    }

    public void AdvanceLevel()
    {
        LevelState++;
    }

    public void LoadLevel(bool fadeOut, bool useLoadingScreen)
    {
        if (LevelState == levels.Count) {
            Debug.Log("The End"); // ggwp
            return; 
        }

        if (SceneManager.GetActiveScene().name != levels[LevelState])
            LoadLevelByName(levels[LevelState], fadeOut, useLoadingScreen);
    }

    public void LoadLevelByName(string levelName, bool fadeOut, bool useLoadingScreen)
    {
        if (GameState.TrySaveCurrentState(Global.Player?.gameObject, out GameState gameState)) {
            currentState = gameState;
        }

        Debug.Log("Loading " + levelName);

        if (fadeOut) {
            LevelFader.FadeOut(levelName, level => {
                if (useLoadingScreen) {
                    ShowLoadingScreen();
                }

                StartCoroutine(LoadSceneAsync(level));
            });
        }
        else {
            if (useLoadingScreen)
                ShowLoadingScreen();
            
            StartCoroutine(LoadSceneAsync(levelName));
        }
    }

    public void ResetCurrentLevel()
    {
        Level level = GetCurrentLevel();
        level.RespawnEnemies();
        level.RespawnPlayer();
    }

    #region Events

    // Called when an exit is reached on a level.
    public void OnLevelFinished()
    {
        if (GetCurrentLevel().Name == Levels.TownLevel) {
            LoadLevel(true, true);
        } else {
            AdvanceLevel();
            LoadLevelByName(Levels.TownLevel, true, true);
        }
    }

    // Called when async level load has finished.
    void OnSceneLoaded() {
        currentState.Load(Global.Player.gameObject);
        GetCurrentLevel().SpawnPlayer();
        LevelFader.FadeIn(GetCurrentLevel().Name, null);
    }

    #endregion

    void ShowLoadingScreen() {
        SceneManager.LoadScene(Constants.LOADING_SCENE);
    }

    public Level GetCurrentLevel()
    {
        return GameObject.Find("Level").GetComponent<Level>();
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
