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
        
        [Header("Shoker")]
        [SerializeField] private GameObject _shokerButton;
        [SerializeField] private GameObject _shokerBoughtText;
        [SerializeField] private TMP_Text _shokerCostText;
        
        [Header("Documents")]
        [SerializeField] private GameObject _documentsButton;
        [SerializeField] private GameObject _documentsBoughtText;
        [SerializeField] private TMP_Text _documentsCostText;
        
        [Header("COST")]
        [SerializeField] private int _defaultAutoClickCost = 500;
        [SerializeField] private int _defaultVpnCost = 5000;
        [SerializeField] private int _defaultShokerCost = 50000;
        [SerializeField] private int _defaultDocumentCost = 1000000;

        [Header("SOUND")]
        [SerializeField] private AudioSource _audioSourcePay;
        [SerializeField] private AudioSource _audioSourceSosi;
        
        private int _currentAutoClickCost;
        private int _currentVpnCost;
        private int _currentShokerCost;
        private int _currentDocumentCost;
        
        private void Update()
        {
            _currentAutoClickCost = (int)(_defaultAutoClickCost * (1f - G.Passives.DiscountPercent / 100f));
            _currentVpnCost = (int)(_defaultVpnCost * (1f - G.Passives.DiscountPercent / 100f));
            _currentShokerCost = (int)(_defaultShokerCost * (1f - G.Passives.DiscountPercent / 100f));
            _currentDocumentCost = (int)(_defaultDocumentCost * (1f - G.Passives.DiscountPercent / 100f));
            
            _autoClickerCostText.text = $"{_currentAutoClickCost}$";
            _vpnCostText.text = $"{_currentVpnCost}$";
            _shokerCostText.text = $"{_currentShokerCost}$";
            _documentsCostText.text = $"{_currentDocumentCost}$";
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
                _audioSourcePay.Play();
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", _autoClickerButton.transform.position, Color.red);
                _audioSourceSosi.Play();
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
                _audioSourcePay.Play();
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", _vpnButton.transform.position, Color.red);
                _audioSourceSosi.Play();
            }
        }
        
        public void BuyShoker()
        {
            bool isPurchased = _clicker.GetMoneyBag.SpendMoney(_currentShokerCost);

            if (isPurchased)
            {
                _shokerButton.SetActive(false);
                _shokerBoughtText.SetActive(true);

                G.SHOKER.SFASJFPAOPGGOP = true;
                
                Popup.Instance.AddText("Purchased", _shokerButton.transform.position, Color.green);
                _audioSourcePay.Play();
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", _shokerButton.transform.position, Color.red);
                _audioSourceSosi.Play();
            }
        }
        
        public void BuyDocuments()
        {
            bool isPurchased = _clicker.GetMoneyBag.SpendMoney(_currentDocumentCost);

            if (isPurchased)
            {
                _documentsButton.SetActive(false);
                _documentsBoughtText.SetActive(true);

                G.DOCUMENTSAJIFAJIOFJI.AHAHAHAHAHAHAHAAH = true;
                
                Popup.Instance.AddText("Purchased", _documentsButton.transform.position, Color.green);
                _audioSourcePay.Play();
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", _documentsButton.transform.position, Color.red);
                _audioSourceSosi.Play();
            }
        }
    }
}
