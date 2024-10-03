using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    public bool isMyTurn;
    public bool turnStart = false;
    public int maxEnergy = 3;
    public int currentEnergy;
    public int startEnergy;
    public int turnCount = 1;
    public TextMeshProUGUI energyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isMyTurn = true;
        startEnergy = maxEnergy;
        currentEnergy = startEnergy;

        if(isMyTurn)
        {
            Debug.Log("This is my turn");
        }
    }

    private void Update()
    {
        energyText.text = currentEnergy + "/" + startEnergy;
    }

    public void EndTurn()
    {
        isMyTurn = false;
        Debug.Log("Turn End");
        StartCoroutine(EnemyTurnSimulation(2));
    }

    public void StartTurn()
    {
        isMyTurn = true;
        turnStart = true;
        currentEnergy = startEnergy;
        turnCount += 1;
        Debug.Log("Your Turn");
    }

    //for testing
    private IEnumerator EnemyTurnSimulation(int time)
    {
        yield return new WaitForSeconds(time);
        StartTurn();
    }
}
