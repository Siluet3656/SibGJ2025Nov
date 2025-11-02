using System;
using Main.Scripts.View;
using TMPro;

namespace Main.Scripts
{   
    public class MoneyBag
    {
        private MoneyView _moneyView ;
        
        public int CurrentMoney { get; private set; }
        public Action<int> OnMoneyChanged;

        public MoneyBag(TMP_Text uiText)
        {
            _moneyView = new MoneyView(this, uiText);
            
            CurrentMoney = 0;
            OnMoneyChanged?.Invoke(CurrentMoney);
        }
        
        public void GetMoney(int money)
        {
            if (money < 0) return;
            
            CurrentMoney += money;
            
            OnMoneyChanged?.Invoke(CurrentMoney);
        }
        
        public bool SpendMoney(int money)
        {
            if (money < 0) return false;
            
            if (CurrentMoney - money < 0) return false;
            
            CurrentMoney -= money;
            
            OnMoneyChanged?.Invoke(CurrentMoney);

            return true;
        }
    }
}
