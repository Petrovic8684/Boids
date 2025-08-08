using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private KeyCode settingsToggleKey;

    private void Update()
    {
        if (Input.GetKeyDown(settingsToggleKey))
            settingsMenu.SetActive(!settingsMenu.activeSelf);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}