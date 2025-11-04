using Main.Scripts.View;
using System.Collections;
using Main.Scripts.Input;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class Clocks : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _text;       // Часы (время)
        [SerializeField] private TMP_Text _daysText;   // Дата (01.11.2025)
        [SerializeField] private TMP_Text _dayBanner;  // Надпись "День N" посередине экрана
        [SerializeField] private PlayerInputHandler _input;
        [SerializeField] private GameObject _arrow;

        [Header("Time Settings")]
        [SerializeField] private float _switchInterval = 10f; // каждые 10 секунд
        [SerializeField] private int _timeShift = 8;          // игровая скорость
        [SerializeField] private int _speedSlowPercent = 0;   // замедление/ускорение в %

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _amnient;
        
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
        
        private bool _anonimFirstMessage = false;
        private bool _andreyThridMesage;
        private bool anonomSecond;
        private bool _anonomTrid;
        private bool _anoimLAST;
        private bool START = true;

        private void Awake()
        {
            G.GameState = GameState.Night;
            
            _startOfDay = 359;
            _endOfDay = 1400;

            _currentDay = 1;
            _time = _startOfDay;

            _text.text = "05:59";
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

            if (START)
            {
                G.MailSystem.ReceiveMail(
                    LocalizationManager.Instance.Translate("Start_Sender"),
                    LocalizationManager.Instance.Translate("Start_Subj"),
                    LocalizationManager.Instance.Translate("Start_Message")
                );

                G.MailSystem.ReceiveMail(
                    LocalizationManager.Instance.Translate("Start2_Sender"),
                    LocalizationManager.Instance.Translate("Start2_Subj"),
                    LocalizationManager.Instance.Translate("Start2_Message")
                );
                
                START = false;
            }
            
            if (_time > _startOfDay + 30 * _timeShift && !_andreyFirstMessage && _currentDay == 1)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Andrey"),
                    LocalizationManager.Instance.Translate("Andrey1_subj"),
                    LocalizationManager.Instance.Translate("Andrey1_Message"));


                _andreyFirstMessage = true;
            }

            if (_time > _startOfDay + 60 * _timeShift && !_andreySecomdMessage && _currentDay == 1)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Andrey"),
                    LocalizationManager.Instance.Translate("Andrey2_subj"),
                    LocalizationManager.Instance.Translate("Andrey2_Message"));
                
                G.Clicker.GoGamba();
                _andreySecomdMessage = true;
            }

            if (_time > _startOfDay + 15 * _timeShift && !_anonimFirstMessage && _currentDay == 2)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Anonymous"),
                    LocalizationManager.Instance.Translate("Anonymymous1_Subj"),
                    LocalizationManager.Instance.Translate("Anonymymous1_Message"));

                _anonimFirstMessage = true;
            }
            
            if (_currentDay == 2 && _time > _startOfDay + 30 * _timeShift && !G.PoliceEvent.IsActive)
            {
                G.PoliceEvent.StartEvent();
            }

            if (_time > _startOfDay + 15 * _timeShift && !_andreyThridMesage && _currentDay == 3)
            {
                G.VPN.VNP_GOGOGOOGOG = false;
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Andrey"),
                    LocalizationManager.Instance.Translate("Andrey3_Subj"),
                    LocalizationManager.Instance.Translate("Andrey3_Message"));

                _andreyThridMesage = true;
            }

            if (_time > _startOfDay + 45 * _timeShift && !anonomSecond && _currentDay == 3)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Anonymous"),
                    LocalizationManager.Instance.Translate("Anonymymous2_Subj"),
                    LocalizationManager.Instance.Translate("Anonymymous2_Message"));

                anonomSecond = true;
            }

            if (_time > _startOfDay + 15 * _timeShift && !_anonomTrid && _currentDay == 4)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Anonymous"),
                    LocalizationManager.Instance.Translate("Anonymymous2_Subj"),
                    LocalizationManager.Instance.Translate("Anonymymous2_Message"));

                _anonomTrid = true;
                //G.PoliceEvent.StartEvent2();
            }

            if (_time > _startOfDay + 75 * _timeShift && !_anoimLAST && _currentDay == 4)
            {
                G.MailSystem.ReceiveMail(LocalizationManager.Instance.Translate("Anonymous"),
                    LocalizationManager.Instance.Translate("Anonymymous3_Subj"),
                    LocalizationManager.Instance.Translate("Anonymymous3_Message"));

                _anoimLAST = true;
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
            _amnient.Stop();
            G.Clicker.STOP();
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

            _dayBanner.text = $"DAY {dayNumber}";
            _dayBanner.gameObject.SetActive(true);
            _dayBanner.alpha = 1;

            _clicked = false;

            // ждём, пока не кликнут (через новую Input System)
            yield return new WaitUntil(() => _clicked);

            _dayBanner.gameObject.SetActive(false);
        }

        public void NextDay()
        {
            _audioSource.Play();
            _amnient.Play();
            
            _currentDay++;
            _time = _startOfDay;
            int hours = Mathf.FloorToInt(_time / 60);
            int minutes = Mathf.FloorToInt(_time % 60);
            _text.text = $"{hours:00}:{minutes:00}";
            _daysText.text = $"{_currentDay:00}.11.2025";

            if (_currentDay == 2)
            {
                _arrow.SetActive(true);
            }

            if (_currentDay == 3)
            {
                if (G.PoliceEvent.IsPaid == false)
                {
                    SceneManager.LoadScene(2);
                }
            }

            if (_currentDay == 5)
            {
                if (G.DOCUMENTSAJIFAJIOFJI.AHAHAHAHAHAHAHAAH)
                {
                    SceneManager.LoadScene(4);
                }
                else
                {
                    SceneManager.LoadScene(3);
                }
            }
        }
    }
}
