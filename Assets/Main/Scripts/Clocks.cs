using Main.Scripts.View;
using System.Collections;
using Main.Scripts.Input;
using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Clocks : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _text;       // Часы (время)
        [SerializeField] private TMP_Text _daysText;   // Дата (01.11.2025)
        [SerializeField] private TMP_Text _dayBanner;  // Надпись "День N" посередине экрана
        [SerializeField] private PlayerInputHandler _input;

        [Header("Time Settings")]
        [SerializeField] private float _switchInterval = 10f; // каждые 10 секунд
        [SerializeField] private int _timeShift = 8;          // игровая скорость
        [SerializeField] private int _speedSlowPercent = 0;   // замедление/ускорение в %

        private bool _isSlowed;
        private float _timer;
        private int _time;
        private float _timeFloat;
        private int _currentDay;

        private int _startOfDay;
        private int _endOfDay;

        // Письма Андрея
        private bool _andreyFirstMessage = false;
        private bool _andreySecomdMessage = false;
        private bool _clicked;

        private void Awake()
        {
            _startOfDay = 359;
            _endOfDay = 1400;

            _currentDay = 1;
            _time = _startOfDay;

            _text.text = "6:00";
            _daysText.text = "01.11.2025";

            if (_dayBanner != null)
                _dayBanner.gameObject.SetActive(false);
        }

        private void Start()
        {
            _input.OnLeftClick += OnLeftClick;
            StartCoroutine(DayIntroSequence());
        }

        private void OnLeftClick()
        {
            _clicked = true;
        }

        private void Update()
        {
            // Если игра на паузе или не в игровом состоянии — время не идёт
            if (G.GameState != GameState.Day) return;
            
            _timeFloat += Time.deltaTime;

            if (_timeFloat >= 1f)
            {
                _time += _timeShift;
                _timeFloat = 0;
            }

            int hours = Mathf.FloorToInt(_time / 60);
            int minutes = Mathf.FloorToInt(_time % 60);

            _text.text = $"{hours:00}:{minutes:00}";

            if (_time >= _endOfDay)
            {
                StartCoroutine(EndDaySequence());
            }

            _speedSlowPercent = G.Passives.SpeedSlowPercent;

            if (_speedSlowPercent > 0)
            {
                _timer += Time.deltaTime;

                if (_timer >= _switchInterval)
                {
                    _timer = 0f;
                    _isSlowed = !_isSlowed;
                    ApplySpeedChange();
                }
            }

            // --- Сообщения Андрея ---
            if (_time > _startOfDay + 30 * _timeShift && !_andreyFirstMessage)
            {
                G.MailSystem.ReceiveMail(
                    "Andrey (Dept. 4)",
                    "Hey, new guy",
                    "Hey, Konstantin!\nWelcome to the pit..."
                );

                _andreyFirstMessage = true;
            }

            if (_time > _startOfDay + 60 * _timeShift && !_andreySecomdMessage)
            {
                G.MailSystem.ReceiveMail(
                    "Andrey (Dept. 4)",
                    "About the Perks",
                    "Hey Konstantin!\nSo, you’ve probably noticed..."
                );

                G.Clicker.GoGamba();
                _andreySecomdMessage = true;
            }
        }

        private void ApplySpeedChange()
        {
            if (_isSlowed)
            {
                Time.timeScale = 1f - (_speedSlowPercent / 100f);
                if (G.Passives.SpeedSlowText.gameObject.activeInHierarchy)
                    Popup.Instance.AddText("Time slowed!!", G.Passives.SpeedSlowText.transform.position, Color.cyan);
            }
            else
            {
                Time.timeScale = 1f + (_speedSlowPercent / 100f);
                if (G.Passives.AutoClickText.gameObject.activeInHierarchy)
                    Popup.Instance.AddText("Time accelerated!!", G.Passives.SpeedSlowText.transform.position, Color.cyan);
            }
        }

        // =============================
        //        ДЕНЬ / НОЧЬ
        // =============================

        private IEnumerator EndDaySequence()
        {
            G.GameState = GameState.Night;
            _time = _startOfDay; // сбрасываем время заранее, чтобы не зациклить Update
            yield return StartCoroutine(G.ScreenFader.FadeOut()); // затемнение

            yield return StartCoroutine(ShowDayBanner(_currentDay + 1)); // показать "День N"

            NextDay(); // увеличить счётчик дня
            yield return StartCoroutine(G.ScreenFader.FadeIn()); // плавное появление
            G.GameState = GameState.Day;
        }

        private IEnumerator DayIntroSequence()
        {
            // Сначала — полностью затемняем экран (если он вдруг не затемнён)
            G.ScreenFader.Clear();
            yield return StartCoroutine(G.ScreenFader.FadeOut());

            // Показываем баннер на чёрном фоне
            yield return StartCoroutine(ShowDayBanner(_currentDay));

            // После клика — плавное появление игрового мира
            yield return StartCoroutine(G.ScreenFader.FadeIn());
        }


        private IEnumerator ShowDayBanner(int dayNumber)
        {
            if (_dayBanner == null) yield break;

            _dayBanner.text = $"ДЕНЬ {dayNumber}";
            _dayBanner.gameObject.SetActive(true);
            _dayBanner.alpha = 1;

            _clicked = false;

            // ждём, пока не кликнут (через новую Input System)
            yield return new WaitUntil(() => _clicked);

            _dayBanner.gameObject.SetActive(false);
        }

        public void NextDay()
        {
            _currentDay++;
            _time = _startOfDay;
            int hours = Mathf.FloorToInt(_time / 60);
            int minutes = Mathf.FloorToInt(_time % 60);
            _text.text = $"{hours:00}:{minutes:00}";
            _daysText.text = $"{_currentDay:00}.11.2025";
        }
    }
}
