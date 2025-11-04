using Main.Scripts.View;
using TMPro;
using UnityEngine;

namespace Main.Scripts
{
    public class Shop : MonoBehaviour
    {
        [Header("Clicker")]
        [SerializeField] private Clicker _clicker;
        [SerializeField] private GameObject _autoClickerButton;
        [SerializeField] private GameObject _autoClickerBoughtText;
        [SerializeField] private TMP_Text _autoClickerCostText;
        
        [Header("Clicker")]
        [SerializeField] private GameObject _vpnButton;
        [SerializeField] private GameObject _vpnBoughtText;
        [SerializeField] private TMP_Text _vpnCostText;
        
        [SerializeField] private int _defaultAutoClickCost = 500;
        [SerializeField] private int _defaultVpnCost = 5000;

        private int _currentAutoClickCost;
        private int _currentVpnCost;
        
        private void Update()
        {
            _currentAutoClickCost = (int)(_defaultAutoClickCost * (1f - G.Passives.DiscountPercent / 100f));
            _currentVpnCost = (int)(_defaultVpnCost * (1f - G.Passives.DiscountPercent / 100f));
            
            _autoClickerCostText.text = $"{_currentAutoClickCost}$";
            _vpnCostText.text = $"{_currentVpnCost}$";
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
                Popup.Instance.AddText("Not enough money to buy", _autoClickerButton.transform.position, Color.red);
            }
        }

        public void BuyVPN()
        {
            bool isPurchased = _clicker.GetMoneyBag.SpendMoney(_currentVpnCost);

            if (isPurchased)
            {
                _vpnButton.SetActive(false);
                _vpnBoughtText.SetActive(true);

                G.VPN.ALVAWEAWEAWAWRAWAWR = true;
                
                Popup.Instance.AddText("Purchased", _vpnButton.transform.position, Color.green);
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", _vpnButton.transform.position, Color.red);
            }
        }
    }
}
