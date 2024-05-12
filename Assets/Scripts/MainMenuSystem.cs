using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public string sceneTarget;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto); 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneTarget);
    }

    public void GoToOptions()
    {
        
    }

    // Quits Game, works only at build TODO: need to add animation to fade out to black
    public void QuitGame()
    {
        StartCoroutine(StartExiting());
    }

        public IEnumerator StartExiting()
    {
        animator.SetBool("ChangingScene",true);
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
