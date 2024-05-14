using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   public GameObject Frame1;
   public GameObject Frame2;
   public GameObject Frame3;
   public GameObject Image;
   public GameObject Button;

   void Start()
   {
      Frame1 = GameObject.Find("Frame1");
      Frame2 = GameObject.Find("Frame2");
      Frame3 = GameObject.Find("Frame3");
      Image = GameObject.Find("Image");
      Button = GameObject.Find("Button");

      SetUIElementSize(Frame1, 1150f, 1085f);
      SetUIElementSize(Frame2, 1150f, 1085f);
      SetUIElementSize(Frame3, 1150f, 1085f);

      DisableFrames();
   }

   public void PlayGame()
   {
     StartCoroutine(Cutscene());
   }

   IEnumerator Cutscene()
   {
      Image.SetActive(false);

      Button.SetActive(false);

      Frame1.SetActive(true);
      yield return new WaitForSeconds(2.5f); 

      Frame1.SetActive(false);
      Frame2.SetActive(true);
      yield return new WaitForSeconds(2.5f); 

      Frame2.SetActive(false);
      Frame3.SetActive(true);
      yield return new WaitForSeconds(2.5f); 

      Frame3.SetActive(false);

      SceneManager.LoadScene("Opening");
   }

   void SetUIElementSize(GameObject uiElement, float width, float height)
   {
      if (uiElement != null)
      {
         RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
         if (rectTransform != null)
         {
            rectTransform.sizeDelta = new Vector2(width, height);
         }
      }
   }

   void DisableFrames()
   {
      if (Frame1 != null)
         Frame1.SetActive(false);

      if (Frame2 != null)
         Frame2.SetActive(false);

      if (Frame3 != null)
         Frame3.SetActive(false);
   }
}
