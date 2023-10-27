using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckOfCardsController : MonoBehaviour
{
    public GameObject[] AllCards => GetCardsObjects();
    [SerializeField]
    public int cardsCountLimit = 1;

    private List<GameObject> _currentCarts = new List<GameObject>();
    public List<GameObject> CurrentCarts
    {
        get
        {
            return _currentCarts;
        }
    }

    void Start()
    {
        GenerateCardsInStart();
        DisplayCards();
    }


    private void DisplayCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var bufferDisplayed = new List<GameObject>();
        var padding = new Vector3();
        foreach (var card in _currentCarts)
        {
            var displayesCard = Instantiate(card);
            displayesCard.transform.position = padding;
            displayesCard.transform.rotation = new Quaternion(180f, 0f, 0f, 0f);
            displayesCard.transform.SetParent(this.gameObject.transform, false);
            bufferDisplayed.Add(displayesCard);
            padding += new Vector3(0, 0, 0.0035f);
        }
        _currentCarts = bufferDisplayed;
    }

    private GameObject[] GetCardsObjects()
    {
        return Resources.LoadAll("Cards")
                        .Select(x => x as GameObject)
                        .ToArray();
    }

    void GenerateCardsInStart()
    {
        var result = GenerateRandomRange(AllCards.Length);

        int counter = 0;
        while (_currentCarts.Count < cardsCountLimit)
        {
            _currentCarts.Add(AllCards[counter]);
            counter++;
        }
    }

    private int[] GenerateRandomRange(int maxCount)
    {
        var random = new System.Random();
        var n = maxCount;
        int[] array = Enumerable.Range(0, n).ToArray();
        for (int i = 0; i < n; i++)
        {
            int j = random.Next(n);
            int x = array[i];
            array[i] = array[j];
            array[j] = x;
        }
        return array;
    }

    public GameObject WithdrawRandomCard()
    {
        _currentCarts = _currentCarts.Select(x => x)
                                     .Where(x => x != null)
                                     .ToList();

        var randomCardIndex = UnityEngine.Random.Range(0, CurrentCarts.Count);
        var card = Instantiate(CurrentCarts[randomCardIndex]);

        Destroy(CurrentCarts[randomCardIndex]);
        _currentCarts.Remove(CurrentCarts[randomCardIndex]);


        return card;

    }
    public void AddCardToDeck(GameObject card)
    {
        _currentCarts.Add(card); // Дублируются карты при дисплее
        DisplayCards();
    }

}
