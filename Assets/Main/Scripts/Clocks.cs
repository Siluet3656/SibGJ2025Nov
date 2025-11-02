using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Clocks : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _daysText;

        private int _time;
        private float _timeFloat;
        
        private int _currentDay;

        private int _startOfDay;
        private int _endOfDay;
        
        private void Awake()
        {
            _startOfDay = 359;
            _endOfDay = 1400;
            
            _currentDay = 0;
            _time = _startOfDay;
            
            _text.text = "6:00";
            _daysText.text = "Day: 0";
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
        }

        public void NextDay()
        {
            _currentDay++;
            _daysText.text = "Day: " + _currentDay;
        }
    }
}
