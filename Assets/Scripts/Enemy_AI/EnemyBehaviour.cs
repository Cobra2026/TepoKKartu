using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Deck enemyDeck;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CycleCard();
        }
    }

    private void CycleCard()
    {
        if (enemyDeck.handPile.Count > 0)
        {
            Card cardToReturn = enemyDeck.handPile[0];
            enemyDeck.handPile.RemoveAt(0);
            enemyDeck.deckPile.Add(cardToReturn);
            cardToReturn.gameObject.SetActive(false);
        }

        enemyDeck.Shuffle();
        enemyDeck.TurnStartDraw(1);
    }
}