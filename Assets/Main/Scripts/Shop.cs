using Main.Scripts.View;
using TMPro;
using UnityEngine;

namespace Main.Scripts
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Clicker _clicker;
        [SerializeField] private GameObject _autoClickerButton;
        [SerializeField] private GameObject _autoClickerBoughtText;
        [SerializeField] private TMP_Text _autoClickerCostText;
        
        [SerializeField] private int _defaultAutoClickCost = 500;

        private int _currentAutoClickCost;
        
        private void Update()
        {
            _currentAutoClickCost = (int)(_defaultAutoClickCost * (1f - G.Passives.DiscountPercent / 100f));
            
            _autoClickerCostText.text = $"{_currentAutoClickCost}$";
        }

        public void BuyAutoClick()
        {
            bool isPurchased = _clicker.GetMoneyBag.SpendMoney(_currentAutoClickCost);

            if (isPurchased)
            {
                _autoClickerButton.SetActive(false);
                _autoClickerBoughtText.SetActive(true);
            
                _clicker.SetAutoClick();   
                
                Popup.Instance.AddText("Purchased", _autoClickerButton.transform.position, Color.green);
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", transform.position, Color.red);
            }
        }
    }
}
