
using UnityEngine;

public class WIN : MonoBehaviour
{

  public GameManager gameManager;
  public AudioSource source;
  public AudioClip clip;

  void Start()
  {
    source = GetComponent<AudioSource>();
    source.clip = clip;
  }

  void OnTriggerEnter2D ()
  {
    gameManager.CompleteLevel();
    Time.timeScale = 0f;
    PlayAudio();
  }

  public void PlayAudio()
  {
    source.Play();
  } 
}
