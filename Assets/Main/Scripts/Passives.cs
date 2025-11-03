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
        
        private readonly float _defaultIncome = 1f;
        private float _incomeBoostPercent = 0f;
        
        private readonly float _defaultRate = 1f;
        private float _rateBoostPercent = 0f;
        
        private int _randomBoostChancePercent;
        private int _autoClickChancePercent;
        private int _discountPercent;

        private void Awake()
        {
            G.Passives = this;
        }

        public TMP_Text RandomBoostText => _randomBoostText;
        public TMP_Text AutoClickText => _autoClickText;
        public int AutoClickChancePercent => _autoClickChancePercent;
        public int DiscountPercent => _discountPercent;

        public void UpdateIncomeBoost(float amount)
        {
            _incomeBoostPercent += amount;
            G.Clicker.SetIncome((int)((_defaultIncome + _incomeBoostPercent) * G.Clicker.DefaultMoneyIncome));
            
            double rounded = Math.Round(_incomeBoostPercent, 1);
            _incomeText.text = $"+{rounded * 100}%";
            Popup.Instance.AddText($"+{Math.Round(amount, 1) * 100}%", _incomeText.transform.position, Color.white);
        }
        
        public void UpdateRateBoost(float amount)
        {
            _rateBoostPercent += amount;
            G.Clicker.SetRate(G.Clicker.DefaultRate / (_defaultRate + _rateBoostPercent));
            
            double rounded = Math.Round(_rateBoostPercent, 2);
            _rateText.text = $"+{rounded * 100}%";
            Popup.Instance.AddText($"+{Math.Round(amount, 1) * 100}%", _rateText.transform.position, Color.white);
        }

        public void UpdateRandomBoostPercent(int amount)
        {
            if (_randomBoostChancePercent >= 100) return;
            
            _randomBoostChancePercent += amount;

            G.Clicker.SetRandomBoostChance(_randomBoostChancePercent);
            
            _randomBoostText.text = $"{_randomBoostChancePercent}%";
            Popup.Instance.AddText("+1%", _randomBoostText.transform.position, Color.white);
        }

        public void UpdateLuck(int luck)
        {
            _luckText.text = $"{luck}";
            Popup.Instance.AddText("+1", _luckText.transform.position, Color.white);
        }

        public void UpdateAutoClick(int autoClick)
        {
            if (_autoClickChancePercent >= 100) return;
            
            _autoClickChancePercent += autoClick;
            
            _autoClickText.text = $"+{_autoClickChancePercent}%";
            Popup.Instance.AddText("+1%", _luckText.transform.position, Color.white);
        }

        public void UpdateDiscount(int i)
        {
            if (_discountPercent >= 100) return;
            
            _discountPercent += i;
            
            _discountText.text = $"-{_discountPercent}%";
            Popup.Instance.AddText("-1%", _discountText.transform.position, Color.white);
        }
    }
}
