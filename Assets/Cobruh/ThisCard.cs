using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string cardName;
    public int power;
    public string cardAttribute;

    public Text nameText;
    public Text powerText;
    public Text attributeText;

    void Start ()
    {
        thisCard [0] = CardDatabase.cardList[thisId];
    }

    void Update ()
    {
        id =thisCard[0].id;
        cardName=thisCard[0].cardName;
        power =thisCard[0].power;
        cardAttribute =thisCard[0].cardAttribute;

        nameText.text = ""+cardName;
        powerText.text = ""+power;
        attributeText.text = " "+cardAttribute;
    }
}