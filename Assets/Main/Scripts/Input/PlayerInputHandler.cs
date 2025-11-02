using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Debugger _debugger;
        
        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerControls();

            _playerControls.Player.LBM.performed += OnLBM;
            _playerControls.Debug.SpeedUp.started += OnSpeedUp;
            _playerControls.Debug.SpeedUp.canceled += OnSpeedUpPerformed;
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        private void OnLBM(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnSpeedUp(InputAction.CallbackContext obj)
        {
            _debugger.SpeedUp();
        }
        
        private void OnSpeedUpPerformed(InputAction.CallbackContext obj)
        {
            _debugger.DefaultTimeScale();
        }
    }
}
