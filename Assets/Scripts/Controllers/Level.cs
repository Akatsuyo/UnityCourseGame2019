using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public string Name { get; private set; }

    Transform map;
    Transform enemies;
    Transform playerSpawn;

    private void Start() {
        Name = SceneManager.GetActiveScene().name;

        map = transform.Find(Constants.MAP_GROUP_NAME);
        enemies = transform.Find(Constants.ENEMIES_GROUP_NAME);
        playerSpawn = transform.Find(Constants.PLAYER_SPAWN_NAME);
    }

    private void Update() {
        
    }

    public void RespawnEnemies() {
        foreach (Transform enemyTr in enemies)
        {
            Enemy enemy = enemyTr.gameObject.GetComponent<Enemy>();
            enemy.Respawn();
        }
    }

    public void RespawnPlayer() {
        PlayerController controller = Global.Player.gameObject.GetComponent<PlayerController>();
        controller.Respawn(playerSpawn.position);
    }

    public void SpawnPlayer() {
        PlayerController controller = Global.Player.gameObject.GetComponent<PlayerController>();
        controller.Spawn(playerSpawn.position);
    }

    public void OnFinished() {
        if (Name == Levels.TutorialLevel) {
            Global.GameController.AchievementSystem.AddProgress("Complete the Tutorial");
        }
        Global.GameController.OnLevelFinished();
    }
}