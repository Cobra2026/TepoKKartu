using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; }

    [SerializeField] private CardPile playerDeck;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Canvas cardCanvas;
    private TurnSystem turnSystem;

    public List<Card> deckPile = new();
    public List<Card> discardPile = new();
    public List<Card> handPile { get; private set; } = new();
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
        turnSystem = TurnSystem.Instance;
        InstantiateDeck(); // -> to instantiate deck at the start of a level
        TurnStartDraw(3);
    }

    private void Update()
    {
        if(turnSystem.turnStart)
        {
            TurnStartDraw(1);
            turnSystem.turnStart = false;
        }
    }

    private void InstantiateDeck()
    {
        for(int i = 0; i < playerDeck.cardsInPile.Count; i++)
        {
            Card card = Instantiate(cardPrefab, cardCanvas.transform);
            card.setUp(playerDeck.cardsInPile[i]);
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

    public void TurnStartDraw(int value)
    {
        
            for (int i = 0; i < value; i++)
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
                    drawnCard.transform.SetParent(GameObject.FindGameObjectWithTag("Hand").transform);
                    deckPile[0].gameObject.SetActive(true);
                    deckPile.RemoveAt(0);
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
