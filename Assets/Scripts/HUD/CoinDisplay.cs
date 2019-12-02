using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public float showSeconds;

    float showTimer;
    bool hideAfterShow;
    bool hidden;

    Animator animator;
    CoinBank coinBank;
    Text coinsText;

    void Awake()
    {
        animator = GetComponent<Animator>();
        coinsText = transform.Find("Coins").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showTimer > 0) {
            showTimer -= Time.deltaTime;
        } else if (hideAfterShow) {
            Hide();
            hideAfterShow = false;
        }
    }

    public void AttachCoinBank(CoinBank coinBank)
    {
        this.coinBank = coinBank;
        coinBank.Changed += OnCoinChange;
        coinsText.text = coinBank.Coins.ToString();
    }

    void OnCoinChange(object sender, CoinChangeEventArgs e)
    {
        // Can animate this too
        coinsText.text = coinBank.Coins.ToString();

        ShowTimed();
    }

    public void ShowTimed()
    {
        showTimer = showSeconds;

        if (!hideAfterShow) {
            hideAfterShow = true;
            Show();
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}
