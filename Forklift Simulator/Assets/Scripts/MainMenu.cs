using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenuPanel;

    [SerializeField]
    GameObject SettingsPanel;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void Back()
    {
        SettingsPanel.SetActive(false);
        {
            MainMenuPanel.SetActive(true);
        }
    }
}
