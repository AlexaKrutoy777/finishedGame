using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    private ScenesController _scenesController;
    private Button _playButton;
    private Button _toLevelsButton;
    private Button _exitButton;


    void Start()
    {
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        _playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        _toLevelsButton = GameObject.Find("ToLevelsButton").GetComponent<Button>();
        _exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        _playButton.onClick.AddListener(() => _scenesController.GoToLevel(ScenesIndexes.Level1));
        _toLevelsButton.onClick.AddListener(() => _scenesController.GoToLevel(ScenesIndexes.LevelsMenu));
        _exitButton.onClick.AddListener(() => _scenesController.Exit());
    }

    
}

