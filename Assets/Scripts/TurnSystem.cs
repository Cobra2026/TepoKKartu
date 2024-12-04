using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

<<<<<<< Updated upstream:Assets/Scripts/TurnSystem.cs
=======
    public CombatPhase currentPhase;

>>>>>>> Stashed changes:Assets/Scripts/Managers/TurnSystem.cs
    public bool isMyTurn;
    public bool turnStart = false;

    public int maxEnergy = 3;
    public int currentEnergy;
    public int startEnergy;
    public int turnCount = 1;

    public Deck playerDeck;
    public Deck enemyDeck;
    public TextMeshProUGUI playerDeckCountText;
    public TextMeshProUGUI playerDiscardCountText;

    public TextMeshProUGUI energyText;
    public PlayAreaManager playArea;
    public CombatManager combatManager;

    private List<(CardEffect effect, Card currentCard, List<Card> cardList, int turnAmount)> scheduledEffects =
        new List<(CardEffect effect, Card currentCard, List<Card> cardList, int turnAmount)>();

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
        combatManager = CombatManager.Instance;


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

        //texts
        energyText.text = currentEnergy + "/" + startEnergy;
        playerDeckCountText.text = playerDeck.deckPile.Count.ToString();
        playerDiscardCountText.text = playerDeck.discardPile.Count.ToString();
    }

    public void EndTurn()
    {
        if (!playArea.hasPlayed && playArea.cardsInPlayArea.Count > 0)
        {

            playArea.PlayAllCards();

        }

        isMyTurn = false;
        StartCoroutine(EnemyTurnSimulation(1));
    }

    public void StartTurn()
    {
        isMyTurn = true;
        turnStart = true;
        currentEnergy = startEnergy;
        turnCount += 1;
        playArea.hasPlayed = false;
<<<<<<< Updated upstream:Assets/Scripts/TurnSystem.cs
=======
        playArea.enemyHasEntered = false;

        playerDeck.TurnStartDraw();
        enemyDeck.TurnStartDraw();

        ExecuteScheduledEffect();
>>>>>>> Stashed changes:Assets/Scripts/Managers/TurnSystem.cs
    }

    private void PlayerMatchStart()
    {
        playerDeck.InstantiateDeck();
        playerDeck.TurnStartDraw(3);
    }

    private void EnemyMatchStart()
    {
        enemyDeck.InstantiateDeck();
        enemyDeck.TurnStartDraw(1);
    }
    
    private void EndTurnDiscard()
    {
        List<Card> cardsToDiscard = new List<Card>();

        foreach(Card card in playArea.cardsInPlayArea)
        {
            cardsToDiscard.Add(card);
        }

        foreach(Card card in cardsToDiscard)
        {
            if(card.cardData.card_Ownership != CardOwnership.Enemy)
            {
                playerDeck.Discard(card);
            }
            else
            {
                enemyDeck.Discard(card);
            }
        }
        playArea.cardsInPlayArea.Clear();
        playArea.playerCardsInPlay.Clear();
    }

<<<<<<< Updated upstream:Assets/Scripts/TurnSystem.cs
    private IEnumerator EnemyTurnSimulation(int time)
=======
    public void RegisterEffect(CardEffect effect, Card currentCard, List<Card> cardList, int turnAmount)
    {
        scheduledEffects.Add((effect, currentCard, cardList, turnAmount));
    }

    public void ExecuteScheduledEffect()
    {
        if (scheduledEffects.Count > 0)
        {
            for (int i = scheduledEffects.Count - 1; i >= 0; i--)
            {
                var (effect, currentCard, cardList, turnAmount) = scheduledEffects[i];
                if (turnAmount == 0)
                {
                    scheduledEffects.RemoveAt(i);

                }
                else
                {
                    effect.ExecuteEffect(currentCard, cardList);
                    scheduledEffects[i] = (effect, currentCard, cardList, turnAmount - 1);
                }
            }
        }
    }

    private void ActivateAllCards()
    { 
        foreach(var card in playArea.cardsInPlayArea)
        {
            card.PlayCard(playArea.cardsInPlayArea);
        }
    }

    private IEnumerator SimulateEndTurn(float time)
>>>>>>> Stashed changes:Assets/Scripts/Managers/TurnSystem.cs
    {
        ActivateAllCards();
        yield return new WaitForSeconds(time * 2);
        combatManager.CalculateDamage();
<<<<<<< Updated upstream:Assets/Scripts/TurnSystem.cs
=======

>>>>>>> Stashed changes:Assets/Scripts/Managers/TurnSystem.cs
        EndTurnDiscard();
        StartTurn();
    }
}
