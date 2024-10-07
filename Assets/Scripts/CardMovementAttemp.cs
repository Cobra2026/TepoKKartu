using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;

public class CardMovementAttemp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //private bool isDragging = false;
    private bool proceedCaroutine;
    private bool isOverPlayArea = false;
    public bool hasFlipped = false;
    private bool isPlayerCard = false;
    private Canvas cardCanvas;
    private RectTransform rectTransform;
    public Card card;
    private GameObject Hand;
    private CardUI cardUI;
    private Vector2 startPosition;
    private GameObject playArea;
    private float random;
    private bool isPointerOverCard = false;
    //private List<Card> cardsInPlay = new List<Card>();

    private readonly string CANVAS_TAG = "CardCanvas";

    private void Start()
    {
        cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        card = GetComponent<Card>();
        cardUI = GetComponent<CardUI>();

        Hand = GameObject.FindGameObjectWithTag("Hand");
        playArea = GameObject.FindGameObjectWithTag("PlayArea");

        proceedCaroutine = true;
    }

  

    private void Update()
    {
        random = Random.Range(0f, 1f);

        if (Input.GetMouseButtonDown(1) && isPointerOverCard && !isOverPlayArea)
        {
            if (proceedCaroutine)
            {
                StartCoroutine(CardRotation());
            }
        }

        if (TurnSystem.Instance.turnStart)
            {
                hasFlipped = false;
            }
    }

    private IEnumerator CardRotation()
    {
        proceedCaroutine = false;


        if(card.cardPosition == CardPosition.Up)
        {
            for(float i = 0; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f)
                {
                    card.cardPosition = CardPosition.Down;

                }
                yield return new WaitForSeconds(0.01f);
            }

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.cardName.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            cardUI.front_CardImage.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        else if(card.cardPosition == CardPosition.Down)
        {
            for(float i = 0; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f)
                {
                    card.cardPosition = CardPosition.Up;
                }
                yield return new WaitForSeconds(0.01f);
            }
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.cardName.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.back_CardImage.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        proceedCaroutine = true;


    }

    private IEnumerator Flip()
    {
        proceedCaroutine = false;


        if (random < 0.5f)
        {
            for (float i = 0; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    card.cardPosition = CardPosition.Down;

                }
                yield return new WaitForSeconds(0.01f);
            }

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.cardName.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            cardUI.front_CardImage.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        else
        {
            for (float i = 0; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    card.cardPosition = CardPosition.Up;
                }
                yield return new WaitForSeconds(0.01f);
            }
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.cardName.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cardUI.back_CardImage.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        proceedCaroutine = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (TurnSystem.Instance.isMyTurn && !hasFlipped && isPlayerCard)
        {
            rectTransform.anchoredPosition += (eventData.delta / cardCanvas.scaleFactor);
            transform.SetParent(cardCanvas.transform, true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(card.cardData.card_Ownership == CardOwnership.Player)
        {
            isPlayerCard = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isOverPlayArea && isPlayerCard)
        {
            transform.SetParent(Hand.transform, false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverCard = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverCard = false;
    }

    public void beginFlip()
    {
        if (isOverPlayArea)
        {

            if (proceedCaroutine)
            {
                StartCoroutine(Flip());
                hasFlipped = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayArea"))
        {
            isOverPlayArea = true;
            transform.SetParent(playArea.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayArea"))
        {
            isOverPlayArea = false;
        }
    }

}
