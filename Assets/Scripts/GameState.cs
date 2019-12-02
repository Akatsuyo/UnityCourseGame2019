using System;
using UnityEngine;

[SerializableAttribute]
public class GameState {
    public int PlayerCoins { get; set; }
    public float PlayerMaxHealth { get; set; }
    public float PlayerHealth { get; set; }
    public int LevelState { get; set; }
    public float PlayerLevelPosX { get; set; }
    public float PlayerLevelPosY { get; set; }

    private GameState() {}

    public void Load(GameObject player, bool overridePlayerPos = false) {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.maxHealth = PlayerMaxHealth;
        playerHealth.HealthValue = PlayerHealth;

        CoinBank playerCoinBank = player.GetComponent<CoinBank>();
        playerCoinBank.Coins = PlayerCoins;

        Global.GameController.LevelState = LevelState;

        if (overridePlayerPos)
            player.transform.position = new Vector2(PlayerLevelPosX, PlayerLevelPosY);
    }

    public static bool TrySaveCurrentState(GameObject player, out GameState gameState) {
        if (player == null) {
            gameState = null;
            return false;
        }  
        
        Health playerHealth = player.GetComponent<Health>();
        CoinBank playerCoinBank = player.GetComponent<CoinBank>();
        
        gameState = new GameState() {
            PlayerCoins = playerCoinBank.Coins,
            PlayerMaxHealth = playerHealth.maxHealth,
            PlayerHealth = playerHealth.HealthValue,
            PlayerLevelPosX = player.transform.position.x,
            PlayerLevelPosY = player.transform.position.y,
            LevelState = Global.GameController.LevelState
        };

        return true;
    }

    public static GameState CreateNewGameState() {
        return new GameState() {
            PlayerCoins = 0,
            PlayerMaxHealth = 100,
            PlayerHealth = 100,
            LevelState = 0,
            PlayerLevelPosX = 0,
            PlayerLevelPosY = 0
        };
    }
}