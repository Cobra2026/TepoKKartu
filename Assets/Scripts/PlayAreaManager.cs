using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject flipButton;
    public List<CardMovementAttemp> cardsInPlayArea = new List<CardMovementAttemp>();
    private TurnSystem turn;

    private void Start()
    {
        turn = TurnSystem.Instance;

        flipButton.SetActive(false);
        flipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Flip";
        flipButton.GetComponent<Button>().onClick.AddListener(FlipAllCards);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayingCard"))
        {
            CardMovementAttemp card = collision.GetComponent<CardMovementAttemp>();
            if(card != null)
            {
                cardsInPlayArea.Add(card);

                if(card.card.cardData.card_Ownership == CardOwnership.Player)
                {
                    PlayEnemyCards();
                }
            }

            if(cardsInPlayArea.Count >= 1 )
            {
                flipButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayingCard"))
        {
            CardMovementAttemp card = collision.GetComponent<CardMovementAttemp>();
            if(card != null)
            {
                cardsInPlayArea.Remove(card);
            }

            if(cardsInPlayArea.Count == 0)
            {
                flipButton.SetActive(false);
            }
        }
    }

    private void FlipAllCards()
    {
        if(turn.isMyTurn && turn.currentEnergy >= 1)
        {
            foreach (CardMovementAttemp card in cardsInPlayArea)
            {
                card.beginFlip();
            }
            turn.currentEnergy -= 1;

        }
        else
        {
            Debug.Log("You don't have enough energy to flip");
        }
    }

    private void PlayEnemyCards()
    {
        Deck enemyDeck = TurnSystem.Instance.enemyDeck.GetComponent<Deck>();

        if(enemyDeck != null)
        {
            foreach(Card enemyCard in enemyDeck.handPile)
            {
                CardMovementAttemp enemyCardMovement = enemyCard.GetComponent<CardMovementAttemp>();
                if(enemyCardMovement != null)
                {
                    enemyCard.transform.SetParent(GameObject.FindGameObjectWithTag("PlayArea").transform, false);
                    enemyCard.transform.localPosition = new Vector2(0, 80);
                    cardsInPlayArea.Add(enemyCardMovement);
                }
            }
        }
    }

    //Test
    private IEnumerator WaitforStartTurn(int time)
    {
        yield return new WaitForSeconds(time);
        turn.StartTurn();
    }
}
