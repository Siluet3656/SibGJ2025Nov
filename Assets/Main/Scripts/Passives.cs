using System;
using Main.Scripts.View;
using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Passives : MonoBehaviour
    {
        [SerializeField] private TMP_Text _incomeText;
        [SerializeField] private TMP_Text _rateText;
        [SerializeField] private TMP_Text _randomBoostText;
        [SerializeField] private TMP_Text _luckText;
        [SerializeField] private TMP_Text _autoClickText;
        [SerializeField] private TMP_Text _discountText;
        [SerializeField] private TMP_Text _teamSpiritText;
        [SerializeField] private TMP_Text _moneyDoublerText;
        [SerializeField] private TMP_Text _speedSlowText;

        [Header("Other")] [SerializeField] private TMP_Text _debtText;
        
        private readonly float _defaultIncome = 1f;
        private float _incomeBoostPercent = 0f;
        
        private readonly float _defaultRate = 1f;
        private float _rateBoostPercent = 0f;
        
        private int _randomBoostChancePercent;
        private int _autoClickChancePercent;
        private int _discountPercent;
        private int _teamSpiritPercent;
        private int _speedSlowPercent;
        
        private long _debtAmount = 1000000000;

        private void Awake()
        {
            G.Passives = this;
        }

        private void Update()
        {
            if (G.Clicker.GetMoneyBag.CurrentMoney * 2 >= _debtAmount)
            {
                DoubleDebt();
            }
        }

        public TMP_Text RandomBoostText => _randomBoostText;
        public TMP_Text AutoClickText => _autoClickText;
        public TMP_Text SpeedSlowText => _speedSlowText;
        public int AutoClickChancePercent => _autoClickChancePercent;
        public int DiscountPercent => _discountPercent;
        public int TeamSpiritPercent => _teamSpiritPercent;
        public int SpeedSlowPercent => _speedSlowPercent;

        public void UpdateIncomeBoost(float amount)
        {
            _incomeBoostPercent += amount;
            G.Clicker.SetIncome((int)((_defaultIncome + _incomeBoostPercent) * G.Clicker.DefaultMoneyIncome));
            
            double rounded = Math.Round(_incomeBoostPercent, 1);
            _incomeText.text = $"+{rounded * 100}%";

            if (_incomeText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText($"+{Math.Round(amount, 1) * 100}%", _incomeText.transform.position, Color.white);
            }
        }
        
        public void UpdateRateBoost(float amount)
        {
            _rateBoostPercent += amount;
            G.Clicker.SetRate(G.Clicker.DefaultRate / (_defaultRate + _rateBoostPercent));
            
            double rounded = Math.Round(_rateBoostPercent, 2);
            _rateText.text = $"+{rounded * 100}%";

            if (_rateText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText($"+{Math.Round(amount, 1) * 100}%", _rateText.transform.position, Color.white);
            }
        }

        public void UpdateRandomBoostPercent(int amount)
        {
            if (_randomBoostChancePercent >= 100) return;
            
            _randomBoostChancePercent += amount;

            G.Clicker.SetRandomBoostChance(_randomBoostChancePercent);
            
            _randomBoostText.text = $"{_randomBoostChancePercent}%";

            if (_randomBoostText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1%", _randomBoostText.transform.position, Color.white);
            }
        }

        public void UpdateLuck(int luck)
        {
            _luckText.text = $"{luck}";

            if (_luckText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1", _luckText.transform.position, Color.white);
            }
        }

        public void UpdateAutoClick(int autoClick)
        {
            if (_autoClickChancePercent >= 100) return;
            
            _autoClickChancePercent += autoClick;
            
            _autoClickText.text = $"+{_autoClickChancePercent}%";

            if (_luckText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1%", _luckText.transform.position, Color.white);
            }
        }

        public void UpdateDiscount(int i)
        {
            if (_discountPercent >= 100) return;
            
            _discountPercent += i;
            
            _discountText.text = $"-{_discountPercent}%";

            if (_luckText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("-1%", _luckText.transform.position, Color.white);
            }
        }

        public void UpdateHungretClicks(int i)
        {
            if (_teamSpiritPercent >= 100) return;
            
            _teamSpiritPercent += i;
            
            _teamSpiritText.text = $"+{_teamSpiritPercent}% each 100 clicks";

            if (_teamSpiritText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1%", _teamSpiritText.transform.position, Color.white);
            }
        }

        public void DoubleDebt()
        {
            _debtAmount *= 2;
            
            _debtText.text = $"{_debtAmount}$";
            
            _moneyDoublerText.text = $"x{_debtAmount / 1000000000} debt";

            if (_moneyDoublerText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("Debt doubled!!", _moneyDoublerText.transform.position, Color.red);
            }
        }

        public void UpdateLuckToken(int i)
        {
            _speedSlowPercent += i;

            _speedSlowText.text = $"+/-{_speedSlowPercent}%";
            
            if (_speedSlowText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1%", _speedSlowText.transform.position, Color.white);
            }
        }
    }
}
