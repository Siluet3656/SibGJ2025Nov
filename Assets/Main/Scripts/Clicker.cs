using System;
using System.Collections;
using Main.Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Main.Scripts
{
    public class Clicker : MonoBehaviour
    {
       [SerializeField] private Image _buttonProgressImage;
       [SerializeField] private TMP_Text _moneyText;
       
       private MoneyBag _moneyBag;
       
       private float _buttonTimeToEarn = 2.5f;
       private int _moneyIncome = 1;
       
       private bool _buttonPressed;
       private bool _isAutoClick;

       private void Awake()
       {
           _moneyBag = new MoneyBag(_moneyText);

           _buttonPressed = false;
           _isAutoClick = false;
       }

       private void Update()
       {
           if (_isAutoClick)
           {
               if (_buttonPressed == false)
               {
                   StartCoroutine(ButtonProgressRoutine());
               }
           }
       }

       private IEnumerator ButtonProgressRoutine()
       {
           float progress = 0;
           _buttonProgressImage.fillAmount = 0f;
           _buttonPressed = true;

           while (progress < _buttonTimeToEarn)
           {
               progress += Time.deltaTime;
               _buttonProgressImage.fillAmount = progress / _buttonTimeToEarn;
               yield return null;
           }

           _buttonProgressImage.fillAmount = 1f;
           _buttonPressed = false;
           
           EarnMoney();
       }

       private void EarnMoney()
       {
           _moneyBag.GetMoney(_moneyIncome);
           _buttonProgressImage.fillAmount = 0f;
           Popup.Instance.AddText($"+{_moneyIncome}$", transform.position, Color.green);
       }

       public MoneyBag GetMoneyBag => _moneyBag;

       public void MainButtonClick()
       {
           if (_buttonPressed == false)
           {
               StartCoroutine(ButtonProgressRoutine());
           }
       }

       public void SetAutoClick()
       {
           _isAutoClick = true;
       }
    }
}
