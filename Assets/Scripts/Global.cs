using UnityEngine;

public static class Global {
    public static GameController GameController
    {
        get
        {
            return GameObject.Find("GameController")?.GetComponent<GameController>();
        }
    }

    public static HudController HudController
    {
        get
        {
            return GameObject.Find("HUD")?.GetComponent<HudController>();
        }
    }

    public static Pooling Pooling
    {
        get
        {
            return GameObject.Find("Pooling")?.GetComponent<Pooling>();
        }
    }

    public static Transform Player
    {
        get
        {
            return GameObject.Find("Stickman")?.transform;
        }
    }
}