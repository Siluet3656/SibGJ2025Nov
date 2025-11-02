using System;
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
        
        private readonly float _defaultIncome = 1f;
        private float _incomeBoostPercent = 0f;
        
        private readonly float _defaultRate = 1f;
        private float _rateBoostPercent = 0f;
        
        private int _randomBoostChancePercent;

        private void Awake()
        {
            G.Passives = this;
        }

        public TMP_Text RandomBoostText => _randomBoostText;

        public void UpdateIncomeBoost(float amount)
        {
            _incomeBoostPercent += amount;
            G.Clicker.SetIncome((int)((_defaultIncome + _incomeBoostPercent) * G.Clicker.DefaultMoneyIncome));
            
            double rounded = Math.Round(_incomeBoostPercent, 1);
            _incomeText.text = $"+{rounded * 100}%";
        }
        
        public void UpdateRateBoost(float amount)
        {
            _rateBoostPercent += amount;
            G.Clicker.SetRate(G.Clicker.DefaultRate / (_defaultRate + _rateBoostPercent));
            
            double rounded = Math.Round(_rateBoostPercent, 2);
            _rateText.text = $"+{rounded * 100}%";
        }

        public void UpdateRandomBoostPercent(int amount)
        {
            if (_randomBoostChancePercent >= 100) return;
            
            _randomBoostChancePercent += amount;

            G.Clicker.SetRandomBoostChance(_randomBoostChancePercent);
            
            _randomBoostText.text = $"{_randomBoostChancePercent}%";
        }

        public void UpdateLuck(int luck)
        {
            _luckText.text = $"{luck}";
        }
    }
}
