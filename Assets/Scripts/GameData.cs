using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData : ISerializationCallbackReceiver
{
    public int playerCurrentHealth;
    public Resolution CurrentResolution;

    [SerializeField] private int resolutionWidth;
    [SerializeField] private int resolutionHeight;
    public GameData()
    {
        this.playerCurrentHealth = 50;
        CurrentResolution = new Resolution();
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
