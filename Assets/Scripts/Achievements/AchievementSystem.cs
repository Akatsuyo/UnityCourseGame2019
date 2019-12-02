using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    List<Achievement> achievements = new List<Achievement>();
    Queue<Achievement> queuedAchievements = new Queue<Achievement>();

    public event EventHandler<AchievementCompletedEventArgs> AchievementCompleted; // Use for display only! (cause: OnDisplayReady)

    private void Start() {
        AddAchievement(new Achievement("Complete the Tutorial", 1));
        AddAchievement(new Achievement("Buy a heal item 3 times", 3));
        AddAchievement(new Achievement("Kill 5 slimes", 5));
    }

    public void AddAchievement(Achievement achievement)
    {
        achievements.Add(achievement);
        achievement.Completed += OnCompletedAchievement;
    }

    public void AddProgress(string achievementName)
    {
        achievements.Find(x => x.Name == achievementName).AddProgress();
    }

    public void QueueAchievementForDisplay(Achievement achievement) {
        queuedAchievements.Enqueue(achievement);
    }

    void OnCompletedAchievement(object sender, EventArgs e)
    {
        Achievement achievement = sender as Achievement;
        AchievementCompleted?.Invoke(this, new AchievementCompletedEventArgs(achievement));
    }

    public void OnDisplayReady()
    {
        if (queuedAchievements.Count > 0)
            AchievementCompleted?.Invoke(this, new AchievementCompletedEventArgs(queuedAchievements.Dequeue()));
    }
}
