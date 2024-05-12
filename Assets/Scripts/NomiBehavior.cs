using Unity.VisualScripting;
using UnityEngine;

public class NomiBehavior : MonoBehaviour
{
    public AudioClip welcomeClip;
    private AudioSource audioSource;
    public GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){ 
           // animator.Play("Locked Door");
            audioSource.clip = welcomeClip;
            audioSource.Play();
        }
    }
}
