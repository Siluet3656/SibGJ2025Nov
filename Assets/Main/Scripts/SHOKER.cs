using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHOKER : MonoBehaviour
{
   public bool SFASJFPAOPGGOP = false;

   private void Awake()
   {
      G.SHOKER = this;
   }

   private void Update()
   {
      if (SFASJFPAOPGGOP)
         G.PoliceEvent._shockerButton.gameObject.SetActive(true);
   }
}
