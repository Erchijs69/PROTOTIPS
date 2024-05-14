using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatExit : MonoBehaviour
{
    public Rigidbody2D rb;
    private MoveLeft[] moveLefts = new MoveLeft[5]; 
    public GameObject wall1;
    public GameObject wall2;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string[] objectNames = { "BG", "WaveBG", "WaveBG1", "WaveBG2", "CloudBG" };
        for (int i = 0; i < objectNames.Length; i++)
        {
            GameObject obj = GameObject.Find(objectNames[i]);
            moveLefts[i] = obj.GetComponent<MoveLeft>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReduceSpeedOverTime());
            Destroy(wall1);
            Destroy(wall2);
        }
    }

    private IEnumerator ReduceSpeedOverTime()
    {
        float startSpeed = 10f;
        float targetSpeed = 0f;
        float duration = 10f; 
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            
            float progress = Mathf.Clamp01((Time.time - startTime) / duration);

            progress = Mathf.Pow(progress, 2); 

            float newSpeed = Mathf.Lerp(startSpeed, targetSpeed, progress);

            
            foreach (MoveLeft moveLeft in moveLefts)
            {
                if (moveLeft != null)
                {
                    moveLeft.speed = newSpeed;
                }
            }

            
            yield return null;
        }
            foreach (MoveLeft moveLeft in moveLefts)
        {
                if (moveLeft != null)
                {
                    moveLeft.speed = targetSpeed;
                }
        }

    }
}
