using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlChanger : MonoBehaviour
{

    public Animator animator;

    public string nextLevelToLoad;

    void Update()
    {
        
    }

    public void FadeToLevel (string levelName)
    {
        nextLevelToLoad = levelName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextLevelToLoad);
    }

}
