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
            if (_time > _startOfDay + 30 * _timeShift && !_andreyFirstMessage && _currentDay == 1)
            {
                G.MailSystem.ReceiveMail( "Andrey (Dept. 4)", "Hey, new guy", "Hey, Konstantin!\n" + "Welcome to the pit. I saw your name pop up on the system logs — guess you’re the new recruit. " + "Don’t worry, it’s not as bad as it looks. (Mostly.)\n" + 
                                                                              "You’ll notice a SHOP terminal unlocked on your panel. You can spend your credits there — buy small upgrades to make your clicking less... painful.\n" + 
                                                                              "Anyway, good luck. Don’t let the CEO mails freak you out." );


                _andreyFirstMessage = true;
            }

            if (_time > _startOfDay + 60 * _timeShift && !_andreySecomdMessage && _currentDay == 1)
            {
                G.MailSystem.ReceiveMail( "Andrey (Dept. 4)", "About the Perks", "Hey Konstantin!\n" + "So, you’ve probably noticed that SHOP terminal I mentioned earlier. Here’s a little insider tip: " + 
                    "some items there aren’t just regular upgrades — they can randomly give you passive perks that boost your clicks automatically.\n" + "There are different tiers: Common, Rare, and [^$&*!]." + 
                    "Nothing dangerous, of course…\n" + "Catch you later, and remember: click smart, not just fast!" );

                G.Clicker.GoGamba();
                _andreySecomdMessage = true;
            }

            if (_time > _startOfDay + 15 * _timeShift && !_anonimFirstMessage && _currentDay == 2)
            {
                G.MailSystem.ReceiveMail(
                    "Anonymous",
                    "Listen carefully",
                    "Listen carefully.\n" +
                    "Your debt cycle just updated — they’re sending a collection unit your way. You don’t have much time.\n" +
                    "If you have enough money, pay it off at the door. If not… hide, or fake the signal. They don’t ask questions, they just ‘enforce’.\n" +
                    "I’ve seen what happens when you ignore the notice. Don’t make the same mistake."
                );

                _anonimFirstMessage = true;
            }
            
            if (_currentDay == 2 && _time > _startOfDay + 30 * _timeShift && !G.PoliceEvent.IsActive)
            {
                G.PoliceEvent.StartEvent();
            }

            if (_time > _startOfDay + 15 * _timeShift && !_andreyThridMesage && _currentDay == 3)
            {
                G.VPN.VNP_GOGOGOOGOG = false;
                G.MailSystem.ReceiveMail(
                    "Andrey (Dept. 4)",
                    "Network Block",
                    "Hey Konstantin!\n" +
                    "Just a quick warning — the system flagged a network block from SKN. " +
                    "If you want to stay online and keep accessing the SHOP terminal, you’ll need a VPN.\n" +
                    "It’s not just about convenience. Without it, some of the perks and upgrades won’t be reachable, " +
                    "and certain automated tasks might fail. \n" +
                    "Grab the VPN from the SHOP before things get messy. Trust me, you don’t want to be caught offline.\n" +
                    "Stay sharp!"
                );

                _andreyThridMesage = true;
            }

            if (_time > _startOfDay + 45 * _timeShift && !anonomSecond && _currentDay == 3)
            {
                G.MailSystem.ReceiveMail(
                    "Anonymous",
                    "Escape Option",
                    "Konstantin,\n" +
                    "The payment route is dangerous. You can escape using temporary documents.\n" +
                    "Steps:\n" +
                    "1. Go to the SHOP terminal.\n" +
                    "2. Buy the 'Fake Documents'.\n" +
                    "3. Activate them before anyone notices… or before they notice *you*.\n" +
                    "Remember, some things aren’t meant to be seen. Move carefully."
                );

                anonomSecond = true;
            }

            if (_time > _startOfDay + 15 * _timeShift && !_anonomTrid && _currentDay == 4)
            {
                G.MailSystem.ReceiveMail(
                    "Anonymous",
                    "Enforcement Incoming",
                    "Konstantin,\n" +
                    "They’re coming again — faster this time, and armed. You’ll need a shocker to survive this encounter.\n" +
                    "Steps:\n" +
                    "1. Go to the SHOP terminal.\n" +
                    "2. Buy the 'Shocker' upgrade.\n" +
                    "3. Keep it ready before they arrive.\n" +
                    "Stay calm, stay hidden, and trust no signals but this one."
                );

                _anonomTrid = true;
                //G.PoliceEvent.StartEvent2();
            }

            if (_time > _startOfDay + 75 * _timeShift && !_anoimLAST && _currentDay == 4)
            {
                G.MailSystem.ReceiveMail(
                    "Anonymous",
                    "Awakening",
                    "Konstantin,\n" +
                    "The day is ending. Whatever you’ve done… it will not matter soon.\n" +
                    "Stay where you are. Watch the shadows. The clicks you’ve made belong to them now.\n" +
                    "Remember me… or don’t. Either way, the system waits."
                );

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
                    SceneManager.LoadScene(1);
                }
            }

            if (_currentDay == 5)
            {
                if (G.DOCUMENTSAJIFAJIOFJI.AHAHAHAHAHAHAHAAH)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}
