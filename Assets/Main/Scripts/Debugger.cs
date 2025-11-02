using UnityEngine;

namespace Main.Scripts
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private float _SpeedUpTimeScale;
        
        private float _defaultTimeScale = 1f;

        public void SpeedUp()
        {
            Time.timeScale = _SpeedUpTimeScale;
            Debug.Log(Time.timeScale);
        }

        public void DefaultTimeScale()
        {
            Time.timeScale = _defaultTimeScale;
            Debug.Log(Time.timeScale);
        }
    }
}
