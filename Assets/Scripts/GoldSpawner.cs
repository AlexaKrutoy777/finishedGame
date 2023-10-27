using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{

    public int currentIngotCount = 5;
    private List<GameObject>  allIngots = new List<GameObject>();
    [SerializeField]
    private GameObject ingotTamplate;
    private IntuitionGameController _intuitionController;
    void Start()
    {
        _intuitionController = GameObject.FindObjectOfType<IntuitionGameController>();
        for(int i = 0; i < currentIngotCount; i++)
        {
            AddIngot();
        }

        _intuitionController.OnQuessed.AddListener(() => { AddIngot(); currentIngotCount++; });
        _intuitionController.OnNotQuessed.AddListener(() => { RemoveIngot(); });
    }

    private void AddIngot()
    {
        var ingot = Instantiate(ingotTamplate);
        ingot.transform.position = this.gameObject.transform.position;

        

        allIngots.Add(ingot);
    }
    private void RemoveIngot()
    {
        var ingot = allIngots.Select(x => x)
                             .Where(x => x != null)
                             .First();

        currentIngotCount = currentIngotCount - 1;

        Destroy(ingot);
    }
}
