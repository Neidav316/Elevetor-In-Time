using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeMachineBehaviour : MonoBehaviour, Interactable
{
    Animator animator;
    AudioSource audioSource;
    public string sceneTarget;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void interact()
    {
        if(!sceneTarget.Equals(""))
        {
            animator.SetTrigger("Press Button");
            audioSource.Play();
            StartCoroutine(startTransition());
        }
    }
    private IEnumerator startTransition()
    {
        GameEventManeger.OnFadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneTarget);
    }
}
