using System.Collections;
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

       private void Awake()
       {
           _moneyBag = new MoneyBag(_moneyText);

           _buttonPressed = false;
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
       }

       public void MainButtonClick()
       {
           if (_buttonPressed == false)
           {
               StartCoroutine(ButtonProgressRoutine());
           }
       }
    }
}
