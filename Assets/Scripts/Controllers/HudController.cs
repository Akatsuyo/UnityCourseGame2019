using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public HealthDisplay HealthDisplay { get; private set; }
    public BulletDisplay BulletDisplay { get; private set; }
    public CoinDisplay CoinDisplay { get; private set; }
    public AchievementDisplay AchievementDisplay { get; private set; }

    void Awake()
    {
        HealthDisplay = transform.Find("HealthDisplay").GetComponent<HealthDisplay>();
        BulletDisplay = transform.Find("BulletDisplay").GetComponent<BulletDisplay>();
        CoinDisplay = transform.Find("CoinDisplay").GetComponent<CoinDisplay>();
        AchievementDisplay = transform.Find("AchievementDisplay").GetComponent<AchievementDisplay>();
    }
}
