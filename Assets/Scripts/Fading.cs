using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    string fadingTo;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveFade()
    {
        animator.SetTrigger("RemoveFade");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut(string nextScene)
    {
        fadingTo = nextScene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadedOut() {
        Utils.GameController.OnFadeOutFinished(fadingTo);
    }

    public void OnFadedIn() {
        Utils.GameController.OnFadeInFinished();
    }
}
