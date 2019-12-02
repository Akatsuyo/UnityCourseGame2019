using System;

public class Achievement {
    public string Name { get; }
    public int NeededCount { get; }

    private int _completedCount;
    public int CompletedCount
    {
        get
        {
            return _completedCount;
        }
        set
        {
            _completedCount = value;
            if (NeededCount == _completedCount) 
                Completed?.Invoke(this, null);
        }
    }

    public event EventHandler Completed;

    public Achievement(string name, int needed)
    {
        Name = name;
        NeededCount = needed;
    }

    public void AddProgress()
    {
        CompletedCount += 1;
    }

    public void AddProgressMultiple(int count)
    {
        CompletedCount += count;
    }
}