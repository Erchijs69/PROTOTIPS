using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine4 : MonoBehaviour
{
    PlayerMovement PM;
    public GameObject player;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        PM = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(PM.TPDestroyed)
        {
            StartCoroutine(LoadLevel());
        }   
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.06f);
        SceneManager.LoadScene("Level5");
        transitionAnim.SetTrigger("Start");
    }
}
