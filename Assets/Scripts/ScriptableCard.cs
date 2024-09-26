using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Data")]
public class ScriptableCard : ScriptableObject
{
    [field: SerializeField] public string card_Name {  get; private set; }
    [field: SerializeField] public int front_Number { get; private set; }
    [field: SerializeField] public int back_Number { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public CardType front_Type { get; private set; }
    [field: SerializeField] public CardType back_Type { get; private set; }
    [field: SerializeField] public CardPosition cardPosition;



}

public enum CardType
{
    Attack,
    Defend
}

public enum CardPosition
{
    Up,
    Down
}
