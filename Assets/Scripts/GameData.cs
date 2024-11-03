using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerCurrentHealth;
    public Resolution CurrentResolution;

    public GameData()
    {
        this.playerCurrentHealth = 50;
        CurrentResolution = new Resolution { width = 1920, height = 1080 };
    }
}