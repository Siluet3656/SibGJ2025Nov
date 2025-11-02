using UnityEngine;
using TMPro;

namespace Main.Scripts
{
    public class Clocks : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _time;
        private float _timeFloat;
        
        private void Awake()
        {
            _text.text = "0:00";
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
        }
    }
}
