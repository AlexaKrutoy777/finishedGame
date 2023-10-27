using TMPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

public class LevelButtonController : MonoBehaviour
{
    private Button _button;
    private TMP_Text _text;
    private ScenesController _scenesController;
    private RawImage _background;
    private TMP_Text _priceUI;
    private int _price;
    private LevelsMenuController _levelsMenuController;

    private bool _isOpen;

    private int _sceneIndex;
    void Awake()
    {
        _levelsMenuController = GameObject.FindObjectOfType<LevelsMenuController>();
        _background = this.gameObject.GetComponent<RawImage>();
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        _button = this.gameObject.GetComponent<Button>();
        _text = this.gameObject.transform.Find("Number").GetComponent<TMP_Text>();
        _priceUI = this.gameObject.transform.Find("Price").GetComponent<TMP_Text>();

        _button.onClick.AddListener(() => OnClickButton());
    }

    public void SetData(string name, int sceneIndex, bool isOpen, int price)
    {
        _sceneIndex = sceneIndex;
        _text.text = name;
        _priceUI.text = price.ToString();
        _price = price;

        //_button.enabled = isOpen;
        _isOpen = isOpen;
        if (!isOpen)
        {
            _background.color = new Color(130, 0, 0, 1);
        }
    }

    private async void OnClickButton()
    {
        if (_isOpen)
        {
            _scenesController.GoToLevel(_sceneIndex);
        } else
        {
            if (_scenesController.IngotCount >= _price)
            {
                _isOpen = true;
                _scenesController.IngotCount -= _price;
                _levelsMenuController.UpdateIngotCount();
                _background.color = new Color(0, 0, 0, 0.509804f);

                _scenesController.OpenScene(_sceneIndex);
                await _scenesController.SaveData();

            }
        }
    }

}
