using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    int magSize = 0;
    int bulletsLoaded = 0;

    Text magSizeText;
    Text bulletsLoadedText;

    // Start is called before the first frame update
    void Awake()
    {
        magSizeText = transform.Find("MagSize").GetComponent<Text>();
        bulletsLoadedText = transform.Find("BulletsLoaded").GetComponent<Text>();
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTexts()
    {
        if (magSize > 0) {
            gameObject.SetActive(true);
            magSizeText.text = magSize.ToString();
            bulletsLoadedText.text = bulletsLoaded.ToString();
        } else {
            gameObject.SetActive(false);
        }
    }

    public void SetMagSize(int magSize)
    {
        this.magSize = magSize;
        UpdateTexts();
    }

    public void SetBullets(int bulletsLoaded)
    {
        this.bulletsLoaded = bulletsLoaded;
        UpdateTexts();
    }

    public void Reset()
    {
        this.magSize = 0;
        UpdateTexts();
    }
}
