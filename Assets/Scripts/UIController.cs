using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject _overlay;
    private GameObject _endWindow;
    private GameObject _finishWindow;
    private IntuitionGameController _intuitionController;
    private ScenesController _sceneController;
    void Start()
    {
        _intuitionController = GameObject.FindObjectOfType<IntuitionGameController>();
        _sceneController = GameObject.FindObjectOfType<ScenesController>();
        _overlay = GameObject.Find("Overlay");
        _endWindow = GameObject.Find("EndWindow");
        _finishWindow = GameObject.Find("FinishWindow");


        _overlay.SetActive(false);
        _endWindow.SetActive(false);
        _finishWindow.SetActive(false);

        _intuitionController.OnEndGame.AddListener(() => { ShowEndGame(); });
        _intuitionController.OnFinishGame.AddListener(() => { ShowNextLevelGame(); });
    }

    public void ShowEndGame()
    {
        _overlay.SetActive(true);
        _endWindow.SetActive(true);
    }

    public void ShowNextLevelGame()
    {
        _overlay.SetActive(true);
        _finishWindow.SetActive(true);
    }





}
