using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementAttemp : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool isDragged;
    private bool proceedCaroutine;
    private bool isFacingDown;
    private Canvas cardCanvas;
    private RectTransform rectTransform;
    private Card card;
    private GameObject Hand;
    //private CardPosition cardPosition;

    private readonly string CANVAS_TAG = "CardCanvas";

    private void Start()
    {
        cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        card = GetComponent<Card>();

        Hand = GameObject.FindGameObjectWithTag("Hand");
        card.transform.SetParent(Hand.transform);

        //cardPosition = CardPosition.Up; 
        proceedCaroutine = true;
        isFacingDown = false;
        
    }

    private void OnMouseDown()
    {
        if (proceedCaroutine)
        {
            StartCoroutine(CardRotation());
        }
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        if (proceedCaroutine)
    //        {
    //            StartCoroutine(CardRotation());
    //        }
    //    }
    //}

    private IEnumerator CardRotation()
    {
        proceedCaroutine = false;

        if(!isFacingDown)
        {
            for(float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f)
                {
                    card.cardData.cardPosition = CardPosition.Up;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if(isFacingDown)
        {
            for(float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f)
                {
                    card.cardData.cardPosition = CardPosition.Down;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        proceedCaroutine = true;

        isFacingDown = !isFacingDown;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += (eventData.delta / cardCanvas.scaleFactor);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragged= false;
    }


}
