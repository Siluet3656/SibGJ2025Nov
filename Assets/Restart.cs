using System;
using Main.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
        private PlayerControls _playerControls;

        private void Awake()
        {
                _playerControls = new PlayerControls();
                _playerControls.Enable();
                _playerControls.Player.LBM.performed += OnLBM;
        }

        private void OnDestroy()
        {
                _playerControls.Disable();
        }

        private void OnLBM(InputAction.CallbackContext obj)
        {
                SceneManager.LoadScene(1);
        }
}
