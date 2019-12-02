using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour, IHealthBar
{
    Image bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("HealthBar").Find("Bar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(float percentage) {
        bar.fillAmount = percentage;
    }
}
