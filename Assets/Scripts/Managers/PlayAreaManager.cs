using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayAreaManager : MonoBehaviour
{
    public static PlayAreaManager Instance { get; private set; }

    [SerializeField] private GameObject flipButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject attackTally;
    [SerializeField] private GameObject defenseTally;

    public List<Card> cardsInPlayArea = new List<Card>();
    public List<Card> playerCardsInPlay = new List<Card>();
    
    private TurnSystem turn;
    private Deck enemyDeck;
    private CombatManager combatManager;
    private TallyController tallyController;

    public bool hasPlayed = false;
    public bool enemyHasEntered = false;


    //Card Holders
    [SerializeField] GameObject playerCardHolder;
    [SerializeField] GameObject enemyCardHolder;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        turn = TurnSystem.Instance;
        combatManager = CombatManager.Instance;
        enemyDeck = TurnSystem.Instance.enemyDeck.GetComponent<Deck>();
        tallyController = TallyController.Instance;

        //play button
        playButton.SetActive(false);
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        playButton.GetComponent<Button>().onClick.AddListener(PlayAllCards);


        //flip button
        flipButton.SetActive(false);
        flipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Flip";
        flipButton.GetComponent<Button>().onClick.AddListener(FlipAllCards);
    }

    private void Update()
    {
        if(hasPlayed)
        {
            playButton.SetActive(false);
            flipButton.SetActive(true);
        }
        else
        {
            flipButton.SetActive(false);
        }

        if (tallyController != null)
        {
            if (tallyController.isTallyActivated)
            {
                attackTally.SetActive(true);
                defenseTally.SetActive(true);
            }
            else
            {
                attackTally.SetActive(false);
                defenseTally.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayingCard"))
        {
            Card card = collision.GetComponent<Card>();

            if (card != null)
            {
                if (!card.cardRotation.hasFlipped)
                {

                    if (card.cardData.card_Ownership == CardOwnership.Player)
                    {
                        playerCardsInPlay.Add(card);
                    }
                }
            }
            else
            {
                return;
            }


            if (cardsInPlayArea.Count >= 1 && !hasPlayed || playerCardsInPlay.Count >= 1 && !hasPlayed)
            {
                playButton.SetActive(true);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayingCard"))
        {
            Card card = collision.GetComponent<Card>();
            CardRotation cardRotation = collision.GetComponent<CardRotation>();

            if (card != null && !card.cardRotation.hasFlipped)
            {
                cardRotation.isOverPlayArea = false;

                if (card.cardData.card_Ownership == CardOwnership.Player)
                {
                    card.RefundEnergy(2);
                    playerCardsInPlay.Remove(card);
                }

            }

            CheckCardsInPlayArea();
            combatManager.TallyNumbers();

            if (cardsInPlayArea.Count == 0)
            {
                playButton.SetActive(false);
            }
        }
    }

    public void CheckCardsInPlayArea()
    {
        cardsInPlayArea.Clear();
        Card[] playerCards = playerCardHolder.GetComponentsInChildren<Card>();
        Card[] enemyCards = enemyCardHolder.GetComponentsInChildren<Card>();
        cardsInPlayArea.AddRange(playerCards);
        cardsInPlayArea.AddRange(enemyCards);
    }

    private void FlipAllCards()
    {
        CheckCardsInPlayArea();
        if(turn.isMyTurn && turn.currentEnergy >= 1)
        {
            StartCoroutine(FlipAndTallyCards(false));
            turn.currentEnergy -= 1;
        }
    }

    public void PlayAllCards()
    {
        CheckCardsInPlayArea();

        if (turn.isMyTurn && !hasPlayed)
        {
            StartCoroutine(FlipAndTallyCards(true));
            hasPlayed = true;
        }
    }

    private IEnumerator FlipAndTallyCards(bool checkFlip)
    {
        foreach(Card card in cardsInPlayArea)
        {
            if(checkFlip)
            {
                if(!card.cardRotation.hasFlipped)
                {
                    card.cardRotation.BeginFlip();
                }
            }
            else
            {
                card.cardRotation.BeginFlip();
            }
        }

        yield return new WaitForSeconds(0.2f);
        combatManager.TallyNumbers();
    }

    public void PlayEnemyCards()
    {
        if(enemyDeck != null)
        {
            foreach(Card enemyCard in enemyDeck.handPile)
            {
                CardRotation cardRotation = enemyCard.GetComponent<CardRotation>();
                cardRotation.isOverPlayArea = true;
                enemyCard.transform.SetParent(enemyCardHolder.transform, false);
            }
        }

        enemyHasEntered = true;
    }

}
