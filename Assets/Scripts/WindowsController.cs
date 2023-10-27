using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class WindowsController : MonoBehaviour
{
    private IntuitionGameController _intuitionController;
    private ScenesController _scenesController;
    private TMP_Text _quessedCount;
    private TMP_Text _notQuessedCount;
    private TMP_Text _goldCount;
    private Button _endReturnToMenu;
    private Button _endRestartButton;

    private Button _finishReturnToMenu;
    private Button _finishRestartButton;
    private Button _finishNextLeveltButton;

    void Awake()
    {
        _intuitionController = GameObject.FindObjectOfType<IntuitionGameController>();
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        _quessedCount = this.gameObject.transform.Find("QuessedCount").gameObject.GetComponent<TMP_Text>();
        _notQuessedCount = this.gameObject.transform.Find("NotQuessedCount").gameObject.GetComponent<TMP_Text>();
        _goldCount = this.gameObject.transform.Find("GoldCount").gameObject.GetComponent<TMP_Text>();
        _endReturnToMenu = GameObject.Find("EndReturnToMenuButton").GetComponent<Button>();
        _endRestartButton = GameObject.Find("EndRestartButton").GetComponent<Button>();

        _finishReturnToMenu = GameObject.Find("FinishReturnToMenuButton").GetComponent<Button>();
        _finishRestartButton = GameObject.Find("FinishRestartButton").GetComponent<Button>();
        _finishNextLeveltButton = GameObject.Find("FinishNextLevelButton").GetComponent<Button>();
        _finishNextLeveltButton.onClick.AddListener(() => { _scenesController.NextLevel(); });

        _intuitionController.OnEndGame.AddListener(UpdateText);
        _intuitionController.OnFinishGame.AddListener(UpdateText);

        _endRestartButton.onClick.AddListener(() => _scenesController.GoToLevel(SceneManager.GetActiveScene().buildIndex));
        _endReturnToMenu.onClick.AddListener(() => _scenesController.GoToLevel(ScenesIndexes.LevelsMenu));

        _finishRestartButton.onClick.AddListener(() => {
            _scenesController.GoToLevel(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("sasa");
        });
        _finishReturnToMenu.onClick.AddListener(() => { 
            _scenesController.GoToLevel(ScenesIndexes.LevelsMenu);
            Debug.Log("sasa"); 
        });
    }

    private void UpdateText()
    {
        _quessedCount.text = _intuitionController.quessedCount.ToString();
        _notQuessedCount.text = _intuitionController.notQuessedCount.ToString();
        _goldCount.text = _intuitionController.goldSpawner.currentIngotCount.ToString();

        if(_scenesController.NextSceneInfo != null)
        {
            _finishNextLeveltButton.gameObject.SetActive(_scenesController.NextSceneInfo.IsOpen);
        }
    }

}
