using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    private TurnSystem turnSystem;
    private PlayAreaManager playArea;

    private void Start()
    {
        turnSystem = TurnSystem.Instance;
        playArea = PlayAreaManager.Instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        CardMovementAttemp cardMovement = droppedItem.GetComponent<CardMovementAttemp>();
        playArea.PlayEnemyCards();

        if (playArea.playerCardsInPlay.Count <= 1)
        {
            cardMovement.newParent = transform;
        }
        else if (playArea.playerCardsInPlay.Count > 1 && turnSystem.currentEnergy >= 2)
        {
            cardMovement.newParent = transform;
            turnSystem.currentEnergy -= 2;
            cardMovement.card.hasUsedPlayEnergy = true;
        }
        else if (playArea.playerCardsInPlay.Count > 1 && turnSystem.currentEnergy < 2)
        {
            Debug.Log("not enough energy to play card");
            
        }


        //Debug.Log("A card is dropped");
    }


}