using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Singleton { get; private set; }
    [SerializeField] PlayerInput _playerInput;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _playerInput.SwitchCurrentActionMap("Gameplay");
    }
}