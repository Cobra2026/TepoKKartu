using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card> ();

    void Awake ()
    {
        cardList.Add (new Card (0, "Blank", 0, "None"));
        cardList.Add (new Card (1, "Exploder", 20, "Barbarian"));
        cardList.Add (new Card (2, "Taskmaster", 15, "Warrior"));
        cardList.Add (new Card (3, "The Guardian", 10, "Wizard"));
        cardList.Add (new Card (4, "Donu", 27, "Warrior"));
        cardList.Add (new Card (5, "Deca", 27, "Warrior"));
        cardList.Add (new Card (6, "Sentry", 18, "Barbarian"));
        cardList.Add (new Card (7, "Snecko", 6, "Wizard"));
        cardList.Add (new Card (8, "Nemesis", 12, "Wizard"));
        cardList.Add (new Card (9, "Slaver", 15, "Barbarian"));
    }
}