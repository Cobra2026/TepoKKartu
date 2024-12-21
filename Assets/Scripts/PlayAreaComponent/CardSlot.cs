using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    private TurnSystem turnSystem;
    private PlayAreaManager playArea;
    private CombatManager combatManager;

    private void Start()
    {
        turnSystem = TurnSystem.Instance;
        playArea = PlayAreaManager.Instance;
        combatManager = CombatManager.Instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        StartCoroutine(CheckCardsInPlay(droppedItem));

    }

    private IEnumerator CheckCardsInPlay(GameObject droppedItem)
    {
        Card droppedCard = droppedItem.GetComponent<Card>();
        CardMovementAttemp cardMovement = droppedItem.GetComponent<CardMovementAttemp>();
        CardRotation cardRotation = droppedItem.GetComponent<CardRotation>();

        playArea.PlayEnemyCards();

        if (playArea.playerCardsInPlay.Count <= 1 && !playArea.hasPlayed)
        {
            cardMovement.newParent = transform;
            cardRotation.isOverPlayArea = true;
        }
        else if (playArea.playerCardsInPlay.Count > 1 && turnSystem.currentEnergy >= 2 && !playArea.hasPlayed)
        {
            cardMovement.newParent = transform;
            droppedCard.ConsumeEnergy(2);
            cardRotation.isOverPlayArea = true;
        }
        else if (playArea.playerCardsInPlay.Count > 1 && turnSystem.currentEnergy < 2 || playArea.hasPlayed)
        {
            yield return null;
        }

        
        yield return null;
        playArea.CheckCardsInPlayArea();
        combatManager.TallyNumbers();
    }

}
