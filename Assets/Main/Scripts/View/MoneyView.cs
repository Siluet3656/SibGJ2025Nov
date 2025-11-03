using TMPro;

namespace Main.Scripts.View
{
    public class MoneyView
    {
        private readonly MoneyBag _moneyBag;
        private readonly TMP_Text _uiText;

        public MoneyView(MoneyBag bag, TMP_Text uiText)
        {
            _uiText = uiText;
            _moneyBag = bag;
            _moneyBag.OnMoneyChanged += ShowMoney;
        }

        private void ShowMoney(long currentMoney)
        {
            _uiText.text = $"Current money: {_moneyBag.CurrentMoney.ToString()} $";
        }
    }
}
