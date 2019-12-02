using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public float radius;

    GameObject shopGui;
    bool opened;

    Health playerHealth;
    CoinBank playerCoins;

    Text playerCoinsText;
    Text healBuyText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = Global.Player.GetComponent<Health>();
        playerCoins = Global.Player.GetComponent<CoinBank>();

        shopGui = transform.Find("ShopGUI").gameObject;
        shopGui.SetActive(false);

        Transform parent = shopGui.transform.Find("Canvas").Find("Border").Find("Background");
        Transform itemsTransform = parent.Find("Items");
        Transform headerTransform = parent.Find("Header");

        playerCoinsText = headerTransform.Find("Coins").Find("Text").GetComponent<Text>();

        healBuyText = itemsTransform.Find("ItemHeal").Find("Background").Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shop")) {
            if (opened) {
                Close();
            } else if (IsPlayerNear()) {
                Open();
            }
        }

        if (playerHealth.GetPercent() == 1) {
            healBuyText.text = "HP Full";
        } else {
            healBuyText.text = "Buy";
        }
    }

    public void OnBuyHeal()
    {
        if (playerHealth.GetPercent() != 1 && playerCoins.HasEnough(1)) {
            playerCoins.RemoveCoins(1);
            playerHealth.Heal(10);
            playerCoinsText.text = playerCoins.Coins.ToString();
            Global.GameController.AchievementSystem.AddProgress("Buy a heal item 3 times");
        }
    }

    bool IsPlayerNear()
    {
        float dist = Vector2.Distance(Global.Player.position, transform.position);

        return dist <= radius;
    }

    void Open()
    {
        playerCoinsText.text = playerCoins.Coins.ToString();
        shopGui.SetActive(true);
        opened = true;
    }

    void Close()
    {
        shopGui.SetActive(false);
        opened = false;
    }
}
