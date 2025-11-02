using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerControls();

            _playerControls.Player.LBM.performed += OnLBM;
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
    }
}
