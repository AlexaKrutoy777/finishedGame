using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntuitionGameController : MonoBehaviour
{
    private ScenesController _scenesController;
    [SerializeField]
    private GameObject quessedDeck;
    [SerializeField]
    private GameObject notQuessedDeck;
    private bool isQuessed = false;

    public int quessedCount = 0;
    public int notQuessedCount = 0;

    private Button redButton;
    private Button blackButton;

    [SerializeField]
    private GameObject deckOfCards;

    [SerializeField]
    private RuntimeAnimatorController cardAnimatorController;
    private Animator cardAnimator;

    private GameObject _currentCard;
    private PlayStep step;

    public GoldSpawner goldSpawner;
    public DeckOfCardsController deckOfCardsController;

    public UnityEvent OnQuessed;
    public UnityEvent OnNotQuessed;


    public UnityEvent OnEndGame;
    public UnityEvent OnFinishGame;

    public AudioSource CardSound;
    public AudioClip cardFlipSound;
    public AudioClip quessedSound;
    public AudioClip notQuessedSound;

    void Start()
    {
        _scenesController = GameObject.FindObjectOfType<ScenesController>();
        deckOfCardsController = GameObject.FindObjectOfType<DeckOfCardsController>();
        redButton = GameObject.Find("RedButton").GetComponent<Button>();
        blackButton = GameObject.Find("BlackButton").GetComponent<Button>();
        goldSpawner = GameObject.FindObjectOfType<GoldSpawner>();
        redButton.onClick.AddListener(() => RedButtonClick());
        blackButton.onClick.AddListener(() => BlackButtonClick());

        OnFinishGame.AddListener(() => UpdateGlobalIngot());

        PickUpCard();
    }

    private void PickUpCard()
    {
        _currentCard = deckOfCards.GetComponent<DeckOfCardsController>().WithdrawRandomCard();
        _currentCard.transform.position = deckOfCards.transform.position;
        _currentCard.transform.rotation = Quaternion.Euler(90f, 90f, 0f);

        cardAnimator = _currentCard.AddComponent<Animator>();
        cardAnimator.runtimeAnimatorController = cardAnimatorController;
        CardFlipSound();
    }

    public void CardFlipSound()
    {
        CardSound.PlayOneShot(cardFlipSound);
    }
    public void QuessedSound()
    {
        CardSound.PlayOneShot(quessedSound);
    }

    public void NotQuessedSound()
    {
        CardSound.PlayOneShot(notQuessedSound);
    }
    private async Task CheckCorrectnessOfAnswer(CardsType type)
    {
        var cardModel = _currentCard.GetComponent<CardsModel>();
        if (cardModel.type == type)
        {
            cardAnimator.SetBool("MoveToQuessedBool", true);
            isQuessed = true;
        }
        else
        {
            cardAnimator.SetBool("MoveToNotQuessedBool", true);
            isQuessed = false;
        }

        CardFlip();
        await Task.Delay(1550);
        CardFlipSound();
    }

    public void Update()
    {
        if (cardAnimator != null)
        {
            if (cardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stop"))
            {
                if (isQuessed)
                {
                    QuessedSound();
                    Destroy(_currentCard.GetComponent<Animator>());
                    _currentCard.transform.position = new Vector3();
                    _currentCard.transform.localPosition = new Vector3();
                    quessedDeck.GetComponent<DeckOfCardsController>().AddCardToDeck(_currentCard);
                    quessedCount++;
                    OnQuessed.Invoke();
                }
                else
                {
                    NotQuessedSound();
                    Destroy(_currentCard.GetComponent<Animator>());
                    _currentCard.transform.position = new Vector3();
                    _currentCard.transform.localPosition = new Vector3();
                    notQuessedDeck.GetComponent<DeckOfCardsController>().AddCardToDeck(_currentCard);
                    notQuessedCount++;
                    OnNotQuessed.Invoke();
                }

                Destroy(_currentCard);
                _currentCard.GetComponent<CardsModel>().IsLastInDeck = deckOfCardsController.CurrentCarts.Count == 0;

                if (goldSpawner.currentIngotCount == 0)
                {
                    OnEndGame.Invoke();
                } else
                {
                    if (deckOfCardsController.CurrentCarts.Count != 0) PickUpCard();
                    if (deckOfCardsController.CurrentCarts.Count == 0 && _currentCard.GetComponent<CardsModel>().IsLastInDeck) OnFinishGame.Invoke();
                }
            }
        }
    }

    public async void UpdateGlobalIngot()
    {
        _scenesController.AddIngot(goldSpawner.currentIngotCount);
        await _scenesController.SaveData();
    }

    private void CardFlip()
    {
        cardAnimator.SetTrigger("CardFlipTrigger");
    }

    private void RedButtonClick()
    {
        CheckCorrectnessOfAnswer(CardsType.Red);
    }

    private void BlackButtonClick()
    {
        CheckCorrectnessOfAnswer(CardsType.Black);
    }

}
