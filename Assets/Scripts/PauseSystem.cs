using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public Image circleHair;
    public Sprite[] cursorHair;
    Text useText;
    public Texture2D cursorTexture;
    public GameObject centerText;
    public bool centerTextConstant = false;
    Animator animator;
    GameObject panel;

    void OnEnable()
    {
        GameEventManeger.FadeOut += FadeOut;
        GameEventManeger.Interact += CanInteract;
        GameEventManeger.NotInteract += CanNotInteract;
    }
    void OnDisable()
    {
        GameEventManeger.FadeOut -= FadeOut;
        GameEventManeger.Interact -= CanInteract;
        GameEventManeger.NotInteract -= CanNotInteract;
    }
    void Start()
    {
        useText = GameObject.Find("UseText").GetComponent<Text>();
        panel = GameObject.Find("CutScenePanel");
        animator = GetComponent<Animator>();
        Cursor.SetCursor(cursorTexture, new Vector2(12f, 12f), CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(DisplayCenterText());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenuUI.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        circleHair.enabled = !isPaused;
    }
    
    // Go To Main Menu, change scene to the main menu.
    public void GoToMainMenu()
    {
        StartCoroutine(ChangetoMainMenu());
    }
    public IEnumerator ChangetoMainMenu()
    {
        animator.SetBool("ChangingScene",true);
        TogglePause();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
    // Quits Game, works only at build TODO: need to add animation to fade out to black
    public void QuitGame()
    {

        StartCoroutine(StartExiting());
    }

    public void FadeOut()
    {
        animator.SetBool("ChangingScene",true);
    }

    public IEnumerator StartExiting()
    {
        animator.SetBool("ChangingScene",true);
        TogglePause();
        yield return new WaitForSeconds(2);
        Application.Quit();
    }

    public void CanInteract()
    {
        circleHair.sprite = cursorHair[1];
        useText.enabled = true;
    }
    
    public void CanNotInteract()
    {
        circleHair.sprite = cursorHair[0];
        useText.enabled = false;
    }

    public IEnumerator DisplayCenterText()
    {
        yield return new WaitForSeconds(3);
        centerText.active = true;
        if(!centerTextConstant)
        {
            yield return new WaitForSeconds(6);
            centerText.active = false;
        }
    }

    public void CutSceneMode()
    {
        
    }
}
