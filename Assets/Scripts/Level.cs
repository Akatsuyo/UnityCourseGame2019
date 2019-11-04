using UnityEngine;

public class Level : MonoBehaviour {

    public Transform player;

    Transform map;
    Transform enemies;
    Transform playerSpawn;

    private void Start() {
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
        PlayerController controller = player.gameObject.GetComponent<PlayerController>();
        controller.Respawn(playerSpawn.position);
    }

    public void OnFinished() {
        Utils.GameController.OnLevelFinished();
    }
}