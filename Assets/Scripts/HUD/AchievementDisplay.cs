using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public float showSeconds;

    float showTimer;
    bool hideAfterShow;
    bool hidden;

    Animator animator;
    Text achievementText;
    Achievement lastAchievement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        achievementText = transform.Find("Image").Find("AchievementName").GetComponent<Text>();
    }

    void Start()
    {
        Global.GameController.AchievementSystem.AchievementCompleted += OnAchievementCompleted;
        hidden = true;
        Global.GameController.AchievementSystem.OnDisplayReady();
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

    void OnDestroy() {
        if (!hidden) {
            Global.GameController.AchievementSystem.QueueAchievementForDisplay(lastAchievement);
        }
    }

    void OnAchievementCompleted(object sender, AchievementCompletedEventArgs e)
    {
        hidden = false;
        lastAchievement = e.Achievement;
        achievementText.text = e.Achievement.Name;
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

    public void OnHidden()
    {
        hidden = true;
        Global.GameController.AchievementSystem.OnDisplayReady();
    }
}
