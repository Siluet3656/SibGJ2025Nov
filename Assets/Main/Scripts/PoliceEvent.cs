using System;
using System.Collections;
using Main.Scripts.Input;
using Main.Scripts.View;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Main.Scripts
{
    public class PoliceEvent : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject _computerHint;    // Подсказка у компа
        [SerializeField] private GameObject _doorHint;        // Подсказка у двери
        [SerializeField] private GameObject _eventPopup;
        [SerializeField] private TMP_Text _eventPopupText;      // Сообщение "Pay: 1000$" + варианты
        [SerializeField] private PlayerInputHandler _input;

        [Header("Event Settings")]
        [SerializeField] private int _payAmount = 1000;
        
        [Header("Popup Buttons")]
        [SerializeField] private GameObject _popupButtons;      // родитель кнопок
        [SerializeField] private UnityEngine.UI.Button _ignoreButton;
        [SerializeField] private UnityEngine.UI.Button _payButton;
        [SerializeField] public UnityEngine.UI.Button _shockerButton;


        private Clicker _clicker;
        private bool _eventActive;
        private bool _doorClicked;
        private bool _paid;
        private bool _popupVisible;
        private bool _clicked;

        private void Awake()
        {
            G.PoliceEvent = this;

            _input.OnLeftClick += OnLeftClick;
        }

        private void Update()
        {
            if (_paid)
            {
                _doorHint.SetActive(false);
                _computerHint.SetActive(false);
            }
        }

        private void Start()
        {
            _clicker = G.Clicker; // теперь точно после Awake всех объектов
            if (_clicker == null)
                Debug.LogError("PoliceEvent: G.Clicker is null!");

            _computerHint.SetActive(false);
            _doorHint.SetActive(false);
            _eventPopup.SetActive(false);
            
            // Подписываем кнопки на действия
            _ignoreButton.onClick.AddListener(OnIgnoreClicked);
            _payButton.onClick.AddListener(OnPayClicked);
            _shockerButton.onClick.AddListener(OnShokerClicked);
        }

        private void OnDestroy()
        {
            _input.OnLeftClick -= OnLeftClick;
        }

        private void OnLeftClick()
        {
            _clicked = true;
        }

        public void StartEvent()
        {
            if (_eventActive) return;
            _eventActive = true;
            StartCoroutine(EventRoutine());
        }
        
        public void StartEvent2()
        {
            if (_eventActive) return;
            _eventActive = true;
            _payAmount = 1000000000;
            StartCoroutine(EventRoutine());
        }
        
        private void OnIgnoreClicked()
        {
            _popupVisible = false;       // закрываем popup
            _eventPopup.SetActive(false);
            _popupButtons.SetActive(false);
            Debug.Log("Police Event ignored");
        }

        private void OnPayClicked()
        {
            if (_clicker.GetMoneyBag.SpendMoney(_payAmount))
            {
                _paid = true;
                _popupVisible = false;
                _eventPopup.SetActive(false);
                _popupButtons.SetActive(false);
                Debug.Log($"Police Event paid {_payAmount}$");
            }
            else
            {
                Debug.Log("Not enough money!");
                // Можно показать красный текст прямо на popup
            }
        }

        private void OnShokerClicked()
        {
            _paid = true;
            _popupVisible = false;
            _eventPopup.SetActive(false);
            _popupButtons.SetActive(false);
            Debug.Log($"Shoker!!");
        }

        private IEnumerator EventRoutine()
        {
            // 1️⃣ Показать визуальные подсказки
            _computerHint.SetActive(true);
            _doorHint.SetActive(true);

            _doorClicked = false;

            // ждём, пока игрок кликнет на дверь
            while (!_doorClicked)
                yield return null;

            // 2️⃣ Показать popup с оплатой
            _eventPopup.SetActive(true);
            _popupButtons.SetActive(true);
            _popupVisible = true;
            _eventPopupText.text = $"Pay {_payAmount}$, else... You have 12 hours";

            // ждём, пока игрок не нажмёт кнопку "Игнорировать" или "Заплатить"
            while (_popupVisible)
                yield return null;

            // 3️⃣ Скрываем подсказки
            _computerHint.SetActive(false);
            _doorHint.SetActive(false);
            _eventPopup.SetActive(false);
            _popupButtons.SetActive(false);

            // 4️⃣ Если не оплатили, конец дня/смерть
            if (!_paid)
            {
                Debug.Log("Player failed the Police Event!");
                // Здесь можно вызвать свою логику смерти
            }

            _eventActive = false;
        }


        public bool IsActive => _eventActive;
        
        public bool IsPaid => _paid;

        
        // Этот метод вызывается из двери в сцене
        public void OnDoorClicked()
        {
            if (_eventActive)
                _doorClicked = true;
        }
    }
}
