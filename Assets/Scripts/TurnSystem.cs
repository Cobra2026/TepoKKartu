using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    public bool isMyTurn;
    public bool turnStart = false;
    public int maxEnergy = 3;
    public int currentEnergy;
    public int startEnergy;
    public int turnCount = 1;
    public Deck playerDeck;
    public Deck enemyDeck;
    public TextMeshProUGUI energyText;
    public PlayAreaManager playArea;

    private void Awake()
    {
        if (Instance != null && Instance != this)
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

        PlayerMatchStart();
        EnemyMatchStart();

        isMyTurn = true;
        startEnergy = maxEnergy;
        currentEnergy = startEnergy;
        
        if(isMyTurn)
        {
            Debug.Log("This is my turn");
        }
    }

    private void Update()
    {
        if (turnStart)
        {
            playerDeck.TurnStartDraw(1);
            enemyDeck.TurnStartDraw(1);
            turnStart = false;
            Debug.Log("turn start");
        }

        energyText.text = currentEnergy + "/" + startEnergy;
    }

    public void EndTurn()
    {
        isMyTurn = false;
        EndTurnDiscard();
        Debug.Log("Turn End");
        StartCoroutine(EnemyTurnSimulation(2));
    }

    public void StartTurn()
    {
        isMyTurn = true;
        turnStart = true;
        currentEnergy = startEnergy;
        turnCount += 1;
        Debug.Log("Your Turn");
    }

    private void PlayerMatchStart()
    {
        playerDeck.InstantiateDeck();
        playerDeck.TurnStartDraw(3);
        Debug.Log("Player draw");
    }

    private void EnemyMatchStart()
    {
        enemyDeck.InstantiateDeck();
        enemyDeck.TurnStartDraw(1);
        Debug.Log("Enemy draw");
    }
    
    private void EndTurnDiscard()
    {
        List<CardMovementAttemp> cardsToDiscard = new List<CardMovementAttemp>();

        foreach(CardMovementAttemp cardMovement in playArea.cardsInPlayArea)
        {
            cardsToDiscard.Add(cardMovement);
        }

        foreach(CardMovementAttemp cardMovement in cardsToDiscard)
        {
            if(cardMovement.card.cardData.card_Ownership != CardOwnership.Enemy)
            {
                playerDeck.Discard(cardMovement.card);
            }
            else
            {
                enemyDeck.Discard(cardMovement.card);
            }
        }
        playArea.cardsInPlayArea.Clear();
    }

    //private void DiscardPlayerCards()
    //{
    //    foreach(CardMovementAttemp playerCard in playArea.playerCardsInPlayArea)
    //    {
    //        playerDeck.Discard(playerCard.card);
    //    }
    //    //playArea.playerCardsInPlayArea.Clear();
    //}

    //private void DiscardEnemyCards()
    //{
    //    foreach(CardMovementAttemp enemyCard in playArea.enemyCardsInPlayArea)
    //    {
    //        enemyDeck.Discard(enemyCard.card);
    //    }
    //    //playArea.enemyCardsInPlayArea.Clear();
    //}

    //for testing
    private IEnumerator EnemyTurnSimulation(int time)
    {
        yield return new WaitForSeconds(time);
        StartTurn();
    }
}
