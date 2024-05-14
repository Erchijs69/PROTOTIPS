using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    public float restartDelay = 1f;
    public bool restart = false;


    public void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            Invoke("Restart", restartDelay);    
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
        restart = true;   
    }

   
}
