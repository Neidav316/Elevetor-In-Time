using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour : MonoBehaviour, HurtableObjects
{
    CharacterController controller; 
    float normalSpeed = 8;
    float sprintSpeed = 12;
    float angularSpeed = 200;
    float angularMultiY = 1.2f; 
    float angularMultiX = 1.1f; 

    float lookUpLimit = -85;
    float lookDownLimit = 60;

    public float maxHealth = 5f;

    public float currentHealth;
    float gravity = 9.8f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    float velocity;

    float jumpPower = 1.0f;
    Quaternion camRotation;
    GameObject aCamera; // must be connected to some object in Unity
    // Start is called before the first frame update
    AudioSource audioSource;
    Animator animator;
    public AudioClip[] footstepSoundClips; 

    HealthBar healthBar;
    

    GameObject itemInHand;
    
    // Lazer Gun Vars
    public GameObject lazer;
    public Transform lazerSpawn;
    public AudioClip lazerShootSound;
    public GameObject lazerGun;
    public float lazerSpeed = 5f;
    public float shootDelay = 0.5f;
    bool canShoot = true;
    float currentTime = 0;
    //
    
    // Sword Vars
    public GameObject sword;
    public GameObject attackPoint;
    public AudioClip SwordSwingSound;
    public float attackRange = 1f;
    public  LayerMask enemyLayers;

    String itemInUsed;


    void Start()
    {
        controller = GetComponent<CharacterController>(); // connect controller to component in Unity
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        itemInHand = GameObject.Find("ItemInHand");
        aCamera = GameObject.Find("Main Camera");
        camRotation = aCamera.transform.localRotation;

        healthBar = GameObject.Find("HealthPanel").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        itemInUsed = "";
    }

    void OnEnable()
    {
        GameEventManeger.ReplanishItemCollected += ReplanishHealth;
    }
    void OnDisable()
    {
        GameEventManeger.ReplanishItemCollected -= ReplanishHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float rotation_about_Y;

        rotation_about_Y = Input.GetAxis("Mouse X")* angularSpeed * Time.deltaTime * angularMultiX;
        transform.Rotate(new Vector3(0, rotation_about_Y, 0));
        camRotation.x += Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime *(-1) * angularMultiY;
        camRotation.x = Mathf.Clamp(camRotation.x,lookUpLimit,lookDownLimit);
        aCamera.transform.localRotation = Quaternion.Euler(camRotation.x,camRotation.y,camRotation.z); // Stops the camera from over rotate 
        float speed = Input.GetKey(KeyCode.LeftShift)? sprintSpeed  : normalSpeed;
        float dx = speed*Time.deltaTime, dz= speed* Time.deltaTime;

        dz *= Input.GetAxis("Vertical");// can be -1, 0 or 1
        dx *= Input.GetAxis("Horizontal");// can be -1, 0 or 1

        Vector3 motion = new Vector3(dx, -0.5f, dz); // in LOCAL coordinates
        motion = transform.TransformDirection(motion); // transforms coordinates to GLOBAL
        controller.Move(motion); // in GLOBAL coordinates

        if(dx != 0 | dz != 0)
        {
            if(!audioSource.isPlaying)
            {
                int index = UnityEngine.Random.Range(0,footstepSoundClips.Length);
                audioSource.clip = footstepSoundClips[index];
                audioSource.Play();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            controller.Move(new Vector3(0,1,0) * jumpPower);
        if (Input.GetMouseButtonDown(0))
            UseItem();
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            ChangeItems();
    }
    private bool IsGrounded() => controller.isGrounded;

    void ReplanishHealth()
    {
        currentHealth += 1;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void ChangeItems()
    {
        if (DataPersistanceManager.Instance.GetGameData().itemsInHand.Contains(sword) && !itemInUsed.Equals("Sword"))
            SetItemInHand(sword);
        else if (DataPersistanceManager.Instance.GetGameData().itemsInHand.Contains(lazerGun) && !itemInUsed.Equals("LazerGun"))
            SetItemInHand(lazerGun);

    }

    public void SetItemInHand(GameObject item)
    {
        if(itemInHand.transform.childCount > 0)
            Destroy(itemInHand.transform.GetChild(0).gameObject);
        GameObject go = Instantiate(item, itemInHand.transform, false);
        itemInUsed = item.name;
    }

    public void UseItem()
    {
        if(itemInUsed == "Sword")
            Swing();
        else if(itemInUsed == "LazerGun")
            Shoot();
    }
    public void Shoot()
    {
        if(canShoot)
        {
            currentTime = Time.time;
            canShoot = false;
            animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(lazer,lazerSpawn.position,lazerSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = lazerSpawn.forward * lazerSpeed;
            audioSource.PlayOneShot(lazerShootSound,0.2f);
        }
        else if(!canShoot && shootDelay - (Time.time - currentTime) <= 0)
            canShoot = true;
        
    
    }
    public void Swing()
    {
        animator.SetTrigger("Swing");
        audioSource.PlayOneShot(SwordSwingSound,0.2f);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position,attackRange,enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<HurtableObjects>().TakeDamage(2f);
            
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
            StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        controller.enabled = false;
        Debug.Log("Player died!");
        // GameEventManeger.Death;
        yield return new WaitForSeconds(2f);

    }
}
