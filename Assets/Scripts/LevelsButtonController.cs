using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsButtonController : MonoBehaviour
{
    private ScenesController _scenesController;
    private List<SceneInfo> _scenesInfo;

    private GameObject _sceneButtonPrefab;
    void Start()
    {
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        _scenesInfo = _scenesController.Scenes;

        _sceneButtonPrefab = Resources.Load("LevelButton") as GameObject;

        foreach(var scene in _scenesInfo)
        {
            var sceneButton = Instantiate(_sceneButtonPrefab);
            sceneButton.GetComponent<LevelButtonController>().SetData(scene.Name, scene.SceneIndex, scene.IsOpen, scene.Price);
            sceneButton.transform.SetParent(this.gameObject.transform, false);
        }
    }
}
