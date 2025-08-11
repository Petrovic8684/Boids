using UnityEngine;

public class GameSettingsToggler : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private KeyCode settingsToggleKey = KeyCode.F1;

    private void Update()
    {
        if (Input.GetKeyDown(settingsToggleKey))
            settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}
