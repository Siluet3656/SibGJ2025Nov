using Main.Scripts.View;
using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Clocks : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _daysText;
        
        [Header("Zavardo")]
        [SerializeField] private int _speedSlowPercent = 0; // замедление/ускорение в %
        [SerializeField] private float _switchInterval = 10f; // каждые 10 секунд
        
        private bool _isSlowed;
        private float _timer;

        private int _time;
        private float _timeFloat;
        
        private int _currentDay;

        private int _startOfDay;
        private int _endOfDay;

        private bool _andreyFirstMessage = false;
        private bool _andreySecomdMessage;

        private void Awake()
        {
            _startOfDay = 359;
            _endOfDay = 1400;
            
            _currentDay = 1;
            _time = _startOfDay;
            
            _text.text = "6:00";
            _daysText.text = "01.11.2025";
        }

        private void Update()
        {
            _timeFloat += Time.deltaTime;

            if (_timeFloat >= 1f)
            {
                _time++;
                _timeFloat = 0;
            }
            
            int hours = Mathf.FloorToInt(_time / 60);
            int minutes = Mathf.FloorToInt(_time % 60);

            if (hours / 10 == 0)
            {
                if (minutes / 10 == 0)
                {
                    _text.text = $"0{hours}:0{minutes}";
                }
                else
                {
                    _text.text = $"0{hours}:{minutes}";
                }
            }
            else
            {
                if (minutes / 10 == 0)
                {
                    _text.text = $"{hours}:0{minutes}";
                }
                else
                {
                    _text.text = $"{hours}:{minutes}";
                }
            }

            if (_time >= _endOfDay)
            {
                NextDay();
                _time = _startOfDay;
            }

            _speedSlowPercent = G.Passives.SpeedSlowPercent;
            
            if (_speedSlowPercent > 0)
            {
                _timer += Time.deltaTime;

                if (_timer >= _switchInterval)
                {
                    _timer = 0f;
                    _isSlowed = !_isSlowed; // переключаем режим
                    ApplySpeedChange();
                }
            }

            if (_time > _startOfDay + 15 && _andreyFirstMessage == false)
            {
                G.MailSystem.ReceiveMail(
                    "Andrey (Dept. 4)",
                    "Hey, new guy",
                    "Hey, Konstantin!\n" +
                    "Welcome to the pit. I saw your name pop up on the system logs — guess you’re the new recruit. " +
                    "Don’t worry, it’s not as bad as it looks. (Mostly.)\n" +
                    "You’ll notice a SHOP terminal unlocked on your panel. You can spend your credits there — buy small upgrades to make your clicking less... painful.\n" +
                    "Anyway, good luck. Don’t let the CEO mails freak you out."
                );

                _andreyFirstMessage = true;
            }
            
            if (_time > _startOfDay + 30 && _andreySecomdMessage == false)
            {
                G.MailSystem.ReceiveMail(
                    "Andrey (Dept. 4)",
                    "About the Perks",
                    "Hey Konstantin!\n" +
                    "So, you’ve probably noticed that SHOP terminal I mentioned earlier. Here’s a little insider tip: " +
                    "some items there aren’t just regular upgrades — they can randomly give you passive perks that boost your clicks automatically.\n" +
                    "There are different tiers: Common, Rare, and [^$&*!]." +
                    "Nothing dangerous, of course…\n" +
                    "Catch you later, and remember: click smart, not just fast!"
                );

                G.Clicker.GoGamba();
                
                _andreySecomdMessage = true;
            }
        }
        
        private void ApplySpeedChange()
        {
            if (_isSlowed)
            {
                // Замедляем
                Time.timeScale = 1f - (_speedSlowPercent / 100f);
                //Debug.Log($"⏳ Замедление на {_speedSlowPercent}% (Time.timeScale = {Time.timeScale})");
                if (G.Passives.SpeedSlowText.gameObject.activeInHierarchy)
                {
                    Popup.Instance.AddText("Time slowed!!", G.Passives.SpeedSlowText.transform.position, Color.cyan);
                }
            }
            else
            {
                // Ускоряем
                Time.timeScale = 1f + (_speedSlowPercent / 100f);
                //Debug.Log($"⚡ Ускорение на {_speedSlowPercent}% (Time.timeScale = {Time.timeScale})");
                if (G.Passives.AutoClickText.gameObject.activeInHierarchy)
                {
                    Popup.Instance.AddText("Time accelerated!!", G.Passives.SpeedSlowText.transform.position, Color.cyan);
                }
            }
        }

        public void NextDay()
        {
            _currentDay++;
            _daysText.text = $"0{_currentDay}.11.2025";
        }
    }
}
