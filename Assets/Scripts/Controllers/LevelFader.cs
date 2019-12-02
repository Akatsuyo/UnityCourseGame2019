using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFader : MonoBehaviour
{
    Animator animator;

    string level;
    Action<string> onAnimFinish;
    bool fading;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoFade()
    {
        animator.SetTrigger("NoFade");
    }

    public void FadeIn(string currentLevel, Action<string> onFadedIn)
    {
        StartFade("FadeIn", currentLevel, onFadedIn);
    }

    public void FadeOut(string nextLevel, Action<string> onFadedOut)
    {
        StartFade("FadeOut", nextLevel, onFadedOut);
    }

    void StartFade(string fadeTrigger, string level, Action<string> onAnimFinish) {
        if (fading) {
            Debug.Log("Already fading!");
            return;
        }

        this.level = level;
        this.onAnimFinish = onAnimFinish;
        fading = true;

        animator.SetTrigger(fadeTrigger);
    }

    void EndFade() {
        if (onAnimFinish != null)
            onAnimFinish(level);
        fading = false;
    }

    // Called when FadeOut animation finishes.
    public void OnFadedOut() {
        EndFade();
    }

    // Called when FadeIn animation finishes.
    public void OnFadedIn() {
        EndFade();
    }
}
