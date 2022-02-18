using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Button restartbutton;

    public Button quitButton;
    

    private void Awake()
    {
        //restartbutton = GetComponent<Button>();
        //quitButton = GetComponent<Button>();
        restartbutton.onClick.AddListener(OnRestartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnQuitButtonClicked()
    {
        AudioManager.Instance.PlayEffect(SoundEnum.ButtonClick);
        SceneManager.LoadScene("Lobby");
    }

    void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
