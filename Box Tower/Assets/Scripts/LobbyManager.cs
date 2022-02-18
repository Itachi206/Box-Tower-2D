using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button playbutton;
    public string sceneName;

    public Button optionButton;
    public Button quitButton;


    private void Awake()
    {
        //playbutton = GetComponent<Button>();
        //quitButton = GetComponent<Button>();
        //optionButton = GetComponent<Button>();
        
        playbutton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        optionButton.onClick.AddListener(OnOptionButtonClicked);
    }

    public void OnOptionButtonClicked()
    {
        AudioManager.Instance.PlayEffect(SoundEnum.ButtonClick);
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.Instance.PlayEffect(SoundEnum.ButtonClick);
        SceneManager.LoadScene("Lobby");
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlayEffect(SoundEnum.ButtonClick);
        SceneManager.LoadScene(sceneName);
}
}

