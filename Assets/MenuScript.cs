using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] InputActionReference _gameplayOpenMenuInput;
    [SerializeField] InputActionReference _menuCloseMenuInput;
    [SerializeField] Canvas _canva;
    private PlayerInput _playerInput;
    bool _buttonPressed;
    bool _menuOpen;
    // Start is called before the first frame update

    private void Awake()
    {
        _gameplayOpenMenuInput.action.performed += OpenMenu;
        _menuCloseMenuInput.action.performed += CloseMenu;

        _playerInput = GetComponent<PlayerInput>();
    }

    private void CloseMenu(InputAction.CallbackContext obj)
    {
        _buttonPressed = true;
        Debug.Log("Fermeture Menu");

        _gameplayOpenMenuInput.action.actionMap.Enable();
        _menuCloseMenuInput.action.actionMap.Disable();

        //_playerInput.SwitchCurrentActionMap("Gameplay");
        //_playerInput.actions["Menu"].performed += SwitchGameplayMap;
    }

    private void OpenMenu(InputAction.CallbackContext obj)
    {
        _buttonPressed = true;
        Debug.Log("Ouverture Menu");

        _gameplayOpenMenuInput.action.actionMap.Disable();
        _menuCloseMenuInput.action.actionMap.Enable();

        //_playerInput.SwitchCurrentActionMap("Menu");
        //_playerInput.actions["Gameplay"].performed += SwitchMenuMap;
    }

    void Start()
    {
        _menuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMenu();
    }

    private void CheckMenu()
    {
        if(_buttonPressed)
        {
            if(!_menuOpen)
            {
                _canva.gameObject.SetActive(true);
                _menuOpen = true;
            }
            else
            {
                _canva.gameObject.SetActive(false);
                _menuOpen = false;
            }
            _buttonPressed=false;
        }
    }

    public void Load()
    {
        SceneManager.LoadScene("SceneElliot");
        _canva.gameObject.SetActive(false);
        _menuOpen = false;
    }
}
