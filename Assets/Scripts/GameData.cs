using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerCurrentHealth;
    // public List<CardMovementAttemp> cardsInPlayArea = new List<CardMovementAttemp>();

    public GameData()
    {
        this.playerCurrentHealth = 50;
        // this.cardsInPlayArea = new List<CardMovementAttemp>();
    }
}
