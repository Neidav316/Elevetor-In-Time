using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour
{
    public string sceneTarget; // Sets the scene target to load at unity editor

    private void OnTriggerEnter(Collider other)
    {
    // gets the scene index from build settings and load the scene by scene target chosen by dev
        SceneManager.LoadScene(sceneTarget);
    }
}