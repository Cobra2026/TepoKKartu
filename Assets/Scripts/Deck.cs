using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
<<<<<<< Updated upstream
    // Singleton (katanya)
    public static Deck Instance { get; private set; }
=======
    //public static Deck Instance { get; private set; }
>>>>>>> Stashed changes

    [SerializeField] private CardPile currentDeck;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Canvas cardCanvas;
    private TurnSystem turnSystem;

    public List<Card> deckPile = new();
    public List<Card> discardPile = new();
    public List<Card> handPile { get; private set; } = new();
<<<<<<< Updated upstream
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InstantiateDeck(); // -> to instantiate deck at the start of a level
        TurnStartDraw();
=======

    private void Start()
    {
        turnSystem = TurnSystem.Instance;
        //InstantiateDeck();
        //TurnStartDraw(3);
>>>>>>> Stashed changes
    }

    private void Update()
    {

    }

    public void InstantiateDeck()
    {
        for(int i = 0; i < currentDeck.cardsInPile.Count; i++)
        {
            Card card = Instantiate(cardPrefab, cardCanvas.transform);
            card.setUp(currentDeck.cardsInPile[i]);
            card.gameObject.SetActive(false);
            deckPile.Add(card);
        }
        Shuffle();

    }

    private void Shuffle()
    {
        for(int i = deckPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = deckPile[i];
            deckPile[i] = deckPile[j];
            deckPile[j] = temp;
        }
    }

<<<<<<< Updated upstream
    public void TurnStartDraw(int value = 4)
    {
        for(int i = 0; i < value; i++)
        {
            if(deckPile.Count <= 0)
=======
    public void TurnStartDraw(int value)
    {
        
            for (int i = 0; i < value; i++)
>>>>>>> Stashed changes
            {
                if (deckPile.Count == 0 && discardPile.Count != 0)
                {
                    deckPile.AddRange(discardPile);
                    discardPile.Clear();
                    Shuffle();
                }

                if (deckPile.Count > 0)
                {
                    Card drawnCard = deckPile[0];
                    handPile.Add(drawnCard);
                    deckPile[0].gameObject.SetActive(true);
                    deckPile.RemoveAt(0);
                    
                    if(drawnCard.cardData.card_Ownership == CardOwnership.Player)
                    {
                        drawnCard.transform.SetParent(GameObject.FindGameObjectWithTag("Hand").transform);
                    }
                    else if(drawnCard.cardData.card_Ownership == CardOwnership.Enemy)
                    {
                        drawnCard.transform.SetParent(GameObject.FindGameObjectWithTag("EnemyArea").transform);
                    }
                }
            }
       

    }

    public void Discard(Card card)
    {
        if(handPile.Contains(card))
        {
            handPile.Remove(card);
            discardPile.Add(card);
            card.gameObject.SetActive(false);   
        }
    }

    public void AdditionalDraw()
    {
        if(TurnSystem.Instance.currentEnergy >= 1)
        {
            TurnStartDraw(1);
            TurnSystem.Instance.currentEnergy -= 1;
        }
        else
        {
            Debug.Log("You dont have enough energy to draw");
        }
    }
}
