using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData : ISerializationCallbackReceiver
{
    public int playerCurrentHealth;
    public Resolution CurrentResolution;
    public List<ScriptableCard> globalDeck = new List<ScriptableCard>();

    [SerializeField] private int resolutionWidth;
    [SerializeField] private int resolutionHeight;

    public GameData()
    {
        this.playerCurrentHealth = 50;
        CurrentResolution = new Resolution();
        globalDeck = new List<ScriptableCard>();
    }

    public void OnBeforeSerialize()
    {
        resolutionWidth = CurrentResolution.width;
        resolutionHeight = CurrentResolution.height;
    }

    public void OnAfterDeserialize()
    {
        CurrentResolution = new Resolution { width = resolutionWidth, height = resolutionHeight };
    }
}