using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RerollButton : MonoBehaviour
{
    [SerializeField] private GameObject flipButton;
    private List<CardMovementAttemp> cardsInPlayArea = new List<CardMovementAttemp>();
    [SerializeField] private TurnSystem turn;

    private void Start()
    {
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
            }

            if(cardsInPlayArea.Count == 1 )
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

            //turn.EndTurn();
            //StartCoroutine(WaitforStartTurn(2));
        }
        else
        {
            Debug.Log("You don't have enough energy to flip");
        }
    }

    //Test
    private IEnumerator WaitforStartTurn(int time)
    {
        yield return new WaitForSeconds(time);
        turn.StartTurn();
    }
}
