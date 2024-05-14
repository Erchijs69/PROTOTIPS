using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 40f;
    public CharacterController2D controller;
    float horizontalMove = 0f;
    bool jump = false;
    public Animator animator;
    public CoinManager cm;
    public bool collect = false;
    public AudioSource source;
    public AudioClip clip;
    public GameObject lantern;
    bool lanternCollected = false;
    private bool lanternIsActive = false;

    //Timer
    Timer timer;
    [SerializeField] GameObject TIMER;

    //SkinBools
    public bool skinA = false;
    public bool skinB = false;
    public bool skinC = false;

    //Teleport
    public bool TPDestroyed = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        animator.SetBool("DefaultJump", true);
    }


    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

       animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

       if (Input.GetButtonDown("Jump"))
       {
            jump = true;
            animator.SetBool("IsJumping", true);
       }

       //ANIMATIONS FOR SKINS
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
            animator.SetBool("Default", true);
            animator.SetBool("DefaultJump", true);
            animator.SetBool("HelloKitty", false);
            animator.SetBool("Banana", false);
            animator.SetBool("Dapper", false);
       }
       if(Input.GetKeyDown(KeyCode.Alpha2) && skinA)
       {
            animator.SetBool("HelloKitty", true);
            animator.SetBool("DefaultJump", false);
            animator.SetBool("Default", false);
            animator.SetBool("Banana", false);
            animator.SetBool("Dapper", false);
       } 
       if (Input.GetKeyDown(KeyCode.Alpha3) && skinB)
       {
            animator.SetBool("Banana", true);
            animator.SetBool("DefaultJump", false);
            animator.SetBool("Default", false);
            animator.SetBool("HelloKitty", false);
            animator.SetBool("Dapper", false);
       }
       if (Input.GetKeyDown(KeyCode.Alpha4) && skinC)
       {
            animator.SetBool("Dapper", true);
            animator.SetBool("DefaultJump", false);
            animator.SetBool("Default", false);
            animator.SetBool("HelloKitty", false);
            animator.SetBool("Banana", false);
       }
       if(cm.coinCount >= 20)
       {
            skinA = true;
       }
       if(cm.coinCount >= 30)
       {
            skinB = true;
       }
       if(cm.coinCount >= 40)
       {
            skinC = true;
       }   
        //Laterna
       if (Input.GetKeyDown(KeyCode.Q) && lanternCollected)
       {
            lanternIsActive = !lanternIsActive;
            lantern.SetActive(lanternIsActive);
       }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectibles"))
        {
            PlayAudio();
            Destroy(other.gameObject);  
            cm.coinCount ++;
            collect = true;
            if(collect)
            {
                Destroy(other.gameObject);
            }
        }    

        if(other.gameObject.CompareTag("TP"))
        {
            StartCoroutine(PlayAnimationAndDestroy(other.gameObject));
        }  
        //laterna
        if(other.gameObject.CompareTag("Lantern"))    
        {
            Destroy(other.gameObject);
            lantern.SetActive(true);
            lanternCollected = true;
        }
    }   

    IEnumerator PlayAnimationAndDestroy(GameObject obj)
    {
        animator.SetBool("IsJumping", false);
        animator.Play("KibbleTP");
        runSpeed = 0;
        yield return new WaitForSeconds(0.9f);
        Destroy(obj);
        TPDestroyed = true;
    }

    IEnumerator PlayDestroy(GameObject obj)
    {
        animator.Play("ReverseTP");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(obj);
    }

    public void PlayAudio()
    {
        source.Play();
    } 

    
} 


