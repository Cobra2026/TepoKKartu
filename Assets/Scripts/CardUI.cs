using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    private Card card;
    //private CardPosition cardPosition;

    //[Header("Prefab elements")]
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image cardTypeBackground;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI frontNumber;
    [SerializeField] private TextMeshProUGUI backNumber;

    [SerializeField] private Sprite attackBackground;
    [SerializeField] private Sprite defendBackground;

    private void Awake()
    {
        card = GetComponent<Card>();
        SetCardUI();
    }

    private void OnValidate()
    {
        Awake();
    }

    private void Update()
    {
        SetCardUI();
    }

    public void SetCardUI()
    {
        if(card != null && card.cardData != null)
        {
            SetCardText();
            SetCardImage();
            SetTypeBackground();
        }
    }

    private void SetCardText()
    {
        cardName.text = card.cardData.card_Name;
        frontNumber.text = card.cardData.front_Number.ToString();
        backNumber.text = card.cardData.back_Number.ToString();
    }

    private void SetCardImage()
    {
        cardImage.sprite = card.cardData.Image;
    }

    private void SetTypeBackground()
    {
        if (card.cardData.cardPosition == CardPosition.Up)
        {
            frontNumber.gameObject.SetActive(true);
            backNumber.gameObject.SetActive(false);
            switch (card.cardData.front_Type)
            {
                case CardType.Attack:
                    cardTypeBackground.sprite = attackBackground;
                    break;
                case CardType.Defend:
                    cardTypeBackground.sprite = defendBackground;
                    break;
            }
        }
        else if (card.cardData.cardPosition == CardPosition.Down)
        {
            frontNumber.gameObject.SetActive(false);
            backNumber.gameObject.SetActive(true);
            switch (card.cardData.back_Type)
            {
                case CardType.Attack:
                    cardTypeBackground.sprite = attackBackground;
                    break;
                case CardType.Defend:
                    cardTypeBackground.sprite = defendBackground;
                    break;
            }
        }
    }

    //private void SetBackTypeBackground()
    //{
    //    switch (card.cardData.back_Type)
    //    {
    //        case CardType.Attack:
    //            cardTypeBackground.sprite = attackBackground;
    //            break;
    //        case CardType.Defend:
    //            cardTypeBackground.sprite = defendBackground;
    //            break;
    //    }
    //}
}
