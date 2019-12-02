using System;

public class AchievementCompletedEventArgs : EventArgs {
    public Achievement Achievement { get; }

    public AchievementCompletedEventArgs(Achievement achievement)
    {
        Achievement = achievement;
    }
}