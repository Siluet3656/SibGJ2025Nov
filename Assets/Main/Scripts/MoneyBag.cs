using System;
using Main.Scripts.View;
using TMPro;

namespace Main.Scripts
{   
    public class MoneyBag
    {
        private MoneyView _moneyView ;
        
        public long CurrentMoney { get; private set; }
        public Action<long> OnMoneyChanged;

        public MoneyBag(TMP_Text uiText)
        {
            _moneyView = new MoneyView(this, uiText);
            
            CurrentMoney = 0;
            OnMoneyChanged?.Invoke(CurrentMoney);
        }
        
        public void GetMoney(long money)
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
