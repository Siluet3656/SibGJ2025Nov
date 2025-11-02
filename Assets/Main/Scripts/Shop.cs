using Main.Scripts.View;
using UnityEngine;

namespace Main.Scripts
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Clicker _clicker;
        [SerializeField] private GameObject _autoClickerButton;
        [SerializeField] private GameObject _autoClickerBoughtText;
        
        [SerializeField] private int _autoClickCost = 500;

        public void BuyAutoClick()
        {
            bool isPurchased = _clicker.GetMoneyBag.SpendMoney(_autoClickCost);

            if (isPurchased)
            {
                _autoClickerButton.SetActive(false);
                _autoClickerBoughtText.SetActive(true);
            
                _clicker.SetAutoClick();   
                
                Popup.Instance.AddText("Purchased", transform.position, Color.green);
            }
            else
            {
                Popup.Instance.AddText("Not enough money to buy", transform.position, Color.red);
            }
        }
    }
}
