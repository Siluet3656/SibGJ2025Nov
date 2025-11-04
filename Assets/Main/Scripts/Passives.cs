using System;
using Main.Scripts.View;
using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Passives : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text _incomeText;
        [SerializeField] private TMP_Text _rateText;
        [SerializeField] private TMP_Text _randomBoostText;
        [SerializeField] private TMP_Text _luckText;
        [SerializeField] private TMP_Text _autoClickText;
        [SerializeField] private TMP_Text _discountText;
        [SerializeField] private TMP_Text _teamSpiritText;
        [SerializeField] private TMP_Text _moneyDoublerText;
        [SerializeField] private TMP_Text _speedSlowText;

        [Header("EmptySpots")]
        [SerializeField] private GameObject _incomeEmptySpot;
        [SerializeField] private GameObject _rateEmptySpot;
        [SerializeField] private GameObject _randomBoostEmptySpot;
        [SerializeField] private GameObject _luckEmptySpot;
        [SerializeField] private GameObject _autoClickEmptySpot;
        [SerializeField] private GameObject _discountEmptySpot;
        [SerializeField] private GameObject _teamSpiritEmptySpot;
        [SerializeField] private GameObject _moneyDoublerEmptySpot;
        [SerializeField] private GameObject _speedSlowEmptySpot;
        
        [Header("Passives")]
        [SerializeField] private GameObject _incomePassive;
        [SerializeField] private GameObject _ratePassive;
        [SerializeField] private GameObject _randomBoostPassive;
        [SerializeField] private GameObject _luckPassive;
        [SerializeField] private GameObject _autoClickPassive;
        [SerializeField] private GameObject _discountPassive;
        [SerializeField] private GameObject _teamSpiritPassive;
        [SerializeField] private GameObject _moneyDoublerPassive;
        [SerializeField] private GameObject _speedSlowPassive;
        
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

            if (_incomePassive.gameObject.activeInHierarchy == false)
            {
                _incomePassive.gameObject.SetActive(true);
                _incomeEmptySpot.SetActive(false);
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
            
            if (_ratePassive.gameObject.activeInHierarchy == false)
            {
                _ratePassive.gameObject.SetActive(true);
                _rateEmptySpot.SetActive(false);
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
            
            if (_randomBoostPassive.gameObject.activeInHierarchy == false)
            {
                _randomBoostPassive.gameObject.SetActive(true);
                _randomBoostEmptySpot.SetActive(false);
            }
        }

        public void UpdateLuck(int luck)
        {
            _luckText.text = $"{luck}";

            if (_luckText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("+1", _luckText.transform.position, Color.white);
            }
            
            if (_luckPassive.gameObject.activeInHierarchy == false)
            {
                _luckPassive.gameObject.SetActive(true);
                _luckEmptySpot.SetActive(false);
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
            
            if (_autoClickPassive.gameObject.activeInHierarchy == false)
            {
                _autoClickPassive.gameObject.SetActive(true);
                _autoClickEmptySpot.SetActive(false);
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
            
            if (_discountPassive.gameObject.activeInHierarchy == false)
            {
                _discountPassive.gameObject.SetActive(true);
                _discountEmptySpot.SetActive(false);
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
            
            if (_teamSpiritPassive.gameObject.activeInHierarchy == false)
            {
                _teamSpiritPassive.gameObject.SetActive(true);
                _teamSpiritEmptySpot.SetActive(false);
            }
        }

        public void DoubleDebt()
        {
            _debtAmount *= 2;
            
            _debtText.text = $"DEBT = {_debtAmount}$";
            
            _moneyDoublerText.text = $"x{_debtAmount / 1000000000} debt";

            if (_moneyDoublerText.gameObject.activeInHierarchy)
            {
                Popup.Instance.AddText("Debt doubled!!", _moneyDoublerText.transform.position, Color.red);
            }
            
            if (_moneyDoublerPassive.gameObject.activeInHierarchy == false)
            {
                _moneyDoublerPassive.gameObject.SetActive(true);
                _moneyDoublerEmptySpot.SetActive(false);
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
            
            if (_speedSlowPassive.gameObject.activeInHierarchy == false)
            {
                _speedSlowPassive.gameObject.SetActive(true);
                _speedSlowEmptySpot.SetActive(false);
            }
        }
    }
}
