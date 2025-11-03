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
       [SerializeField] private Image _hungretClicksImage;
       
       private MoneyBag _moneyBag;
       
       private float _buttonTimeToEarn;
       
       private int _defaultIncome = 1;
       private float _defaultTimeToEarn  = 2.5f;
       private int _moneyIncome;
       
       private bool _buttonPressed;
       private bool _isAutoClick;
       private bool _isGambling;
       private bool _isIncomeBoosted;

       private int _chanceOfRandomBoost;
       private int _hungretClicksProgress;
       private int _hungretClicksIncomePercent;

       private void Awake()
       {
           G.Clicker = this;
           
           _moneyBag = new MoneyBag(_moneyText);

           _buttonPressed = false;
           _isAutoClick = false;
           _isGambling = true;

           _moneyIncome = _defaultIncome;
           _buttonTimeToEarn = _defaultTimeToEarn;
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

           _hungretClicksIncomePercent = G.Passives.TeamSpiritPercent;
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
           
           if (_isIncomeBoosted)
           {
               int rand = Random.Range(0, 100);

               if (rand < G.Passives.AutoClickChancePercent)
               {
                   if (G.Passives.AutoClickText.gameObject.activeInHierarchy)
                   {
                       Popup.Instance.AddText("proc!!", G.Passives.AutoClickText.transform.position, Color.white);
                   }

                   for (int i = 0; i < 5; i++)
                   {
                       EarnMoney(2);
                   }
               }
               else
               {
                   EarnMoney(2);
               }
           }
           else
           {
               int rand = Random.Range(0, 100);

               if (rand < G.Passives.AutoClickChancePercent)
               {
                   if (G.Passives.AutoClickText.gameObject.activeInHierarchy)
                   {
                       Popup.Instance.AddText("proc!!", G.Passives.AutoClickText.transform.position, Color.white);
                   }

                   for (int i = 0; i < 5; i++)
                   {
                       EarnMoney(1);
                   }
               }
               else
               {
                   EarnMoney(1);
               }
           }

           if (_isGambling)
           {
               G.Gambling.Gamba();
           }
       }

       private void EarnMoney(int multiplier)
       {
           
           _moneyBag.GetMoney(_moneyIncome * multiplier);
           _buttonProgressImage.fillAmount = 0f;
           
           Vector2 randPos = new Vector2(transform.position.x + Random.Range(-100,100) / 100f, transform.position.y + Random.Range(-100,100) / 100f);

           if (_buttonProgressImage.gameObject.activeInHierarchy)
           {
               Popup.Instance.AddText($"+{_moneyIncome * multiplier}$", randPos, Color.green);
           }

           int rand = Random.Range(0, 100);

           if (rand < _chanceOfRandomBoost)
           {
               if (_isIncomeBoosted == false)
               {
                  StartCoroutine(BoostIncome());
               }
           }
           
           if (_hungretClicksIncomePercent > 0)
           {
               _hungretClicksProgress++;
               _hungretClicksImage.fillAmount = _hungretClicksProgress / 100f;

               if (_hungretClicksProgress >= 100)
               {
                   _hungretClicksProgress = 0;
                   _hungretClicksImage.fillAmount = 0f;

                   int incomeHere = (int)(_moneyBag.CurrentMoney * (_hungretClicksIncomePercent / 100f));
                   
                   _moneyBag.GetMoney(incomeHere);

                   if (_hungretClicksImage.gameObject.activeInHierarchy)
                   {
                       Popup.Instance.AddText($"+{incomeHere}$", _hungretClicksImage.transform.position, Color.green);
                   }
               }
           }
       }

       private IEnumerator BoostIncome()
       {
           _isIncomeBoosted = true;

           if (G.Passives.RandomBoostText.gameObject.activeInHierarchy)
           {
               Popup.Instance.AddText("BOOSTED!!", G.Passives.RandomBoostText.transform.position, Color.cyan);
           }

           float progress = 0;

           while (progress < 5f)
           {
               progress += Time.deltaTime;
               yield return null;
           }

           if (G.Passives.RandomBoostText.gameObject.activeInHierarchy)
           {
               Popup.Instance.AddText("Boots is gone...", G.Passives.RandomBoostText.transform.position, Color.white);
           }

           _isIncomeBoosted = false;
       }

       public MoneyBag GetMoneyBag => _moneyBag;
       public int DefaultMoneyIncome => _defaultIncome;
       public float DefaultRate => _defaultTimeToEarn;

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

       public void SetIncome(int amount)
       {
           _moneyIncome = amount;
       }

       public void SetRate(float rate)
       {
           if (rate <= 0.05f) return;
           
           _buttonTimeToEarn = rate;
       }

       public void SetRandomBoostChance(int amount)
       {
           if (amount >= 100) return;
           
           _chanceOfRandomBoost = amount;
       }
    }
}
