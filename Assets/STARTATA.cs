using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STARTATA : MonoBehaviour
{
   public Button English;
   public Button Russian;
   public GameObject intro;

   private void Awake()
   {
      English.onClick.AddListener(SetEnglishText);
      Russian.onClick.AddListener(SetRussianText);
   }

   void SetEnglishText()
   {
      LocalizationManager.currentLanguage = SystemLanguage.English;
      intro.SetActive(true);
      gameObject.SetActive(false);
   }
   
   void SetRussianText()
   {
      LocalizationManager.currentLanguage = SystemLanguage.Russian;
      intro.SetActive(true);
      gameObject.SetActive(false);
   }
}
