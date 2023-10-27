using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsMenuController : MonoBehaviour
{
    private ScenesController _scenesController;
    private Button _returnToMainMenu;
    private TMP_Text _ingotCount;

    void Start()
    {
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        _returnToMainMenu = GameObject.Find("ReturnToMainMenuButton").GetComponent<Button>();
        _ingotCount = GameObject.Find("IngotCount").GetComponent<TMP_Text>();

        _returnToMainMenu.onClick.AddListener(() => _scenesController.GoToLevel(ScenesIndexes.Menu));
        UpdateIngotCount();
    }

    public void UpdateIngotCount()
    {
        _ingotCount.text = _scenesController.IngotCount.ToString();
    }
}


