using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    [HideInInspector] public PlayAreaManager playArea;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public AudioManager audioManager;

    [Header("Health Bar Component")]
    [SerializeField] private HealthBarUI playerHealthBar;
    [SerializeField] private HealthBarUI enemyHealthBar;

    [Header("Shaking Component")]
    public ObjectShake playerComponent;
    public ObjectShake enemyComponent;

    [Header("Damage Popup Component")]
    [SerializeField] private DamagePopup enemyDamagePopup;
    [SerializeField] private DamagePopup playerDamagePopup;

    public int playerShield = 0;
    public int enemyShield = 0;
    public int playerTotalAttack = 0;
    public int enemyTotalAttack = 0;

    public bool isTallying = false;

    public void OnEnable()
    {
        CombatEvents.OnPlayerDamageTaken += OnPlayerDamageTaken;
        CombatEvents.OnEnemyDamageTaken += OnEnemyDamageTaken;
    }

    public void OnDisable()
    {
        CombatEvents.OnPlayerDamageTaken -= OnPlayerDamageTaken;
        CombatEvents.OnEnemyDamageTaken -= OnEnemyDamageTaken;
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        playArea = PlayAreaManager.Instance;
        playerHealth = PlayerHealth.Instance;
        enemyHealth = EnemyHealth.Instance;
        audioManager = AudioManager.Instance;

        playerHealthBar.SetMaxHealthBar(playerHealth.playerCurrentHealth, playerHealth.playerMaxHealth);
        StartCoroutine(InitializeEnemyHealth());
    }

    public void TallyNumbers()
    {
        isTallying = true;

        int playerTotalDefense = 0;
        int enemyTotalDefense = 0;
        playerTotalAttack = 0;
        enemyTotalAttack = 0;

        foreach (Card card in playArea.cardsInPlayArea)
        {
            int cardValue = 0;

            CardType currentType = card.CurrentCardType();

            if (card.cardPosition == CardPosition.Up)
            {
                cardValue = card.tempFrontNumber;
            }
            else
            {
                cardValue = card.tempBackNumber;
            }

            if (currentType == CardType.Attack)
            {
                if (card.cardData.card_Ownership == CardOwnership.Player)
                {
                    playerTotalAttack += cardValue;
                }
                else if (card.cardData.card_Ownership == CardOwnership.Enemy)
                {
                    enemyTotalAttack += cardValue;
                }
            }

            else if (currentType == CardType.Defend)
            {
                if (card.cardData.card_Ownership == CardOwnership.Player)
                {
                    playerTotalDefense += cardValue;
                }
                else if (card.cardData.card_Ownership == CardOwnership.Enemy)
                {
                    enemyTotalDefense += cardValue;
                }
            }
        }
        playerShield = playerTotalDefense;
        enemyShield = enemyTotalDefense;

        isTallying = false;
    }

    public void CalculateDamage()
    {
        TallyNumbers();

        if (playerTotalAttack > enemyTotalAttack)
        {
            DealDamageToEnemy(playerTotalAttack);
        }
        else if (playerTotalAttack < enemyTotalAttack)
        {
            DealDamageToPlayer(enemyTotalAttack);
        }
    }

    public void DealDamageToPlayer(int damage)
    {
        if (playerHealth.playerCurrentHealth == 0)
            return;

        if(playerShield > 0)
        {
            if(damage <= playerShield)
            {
                playerShield -= damage;
                damage = 0;
            }
            else
            {
                damage -= playerShield;
                playerShield = 0;
            }
        }

        playerHealth.PlayerTakeDamage(damage);
        ActivatePlayerDamageTaken(damage);
        playerDamagePopup.CreatePopup(damage.ToString());
        playerTotalAttack = 0;


        if (playerHealth.playerCurrentHealth <= 0)
        {
            foreach (var card in playArea.cardsInPlayArea)
            {
                if (card.isBuffed)
                {
                    card.RevertBuff();
                }
            }

            TurnSystem.Instance.SwitchPhase(CombatPhase.PlayerLose);
        }
    }

    public void DealDamageToEnemy(int damage)
    {
        if (enemyHealth.enemyCurrentHealth == 0)
            return;

        if(enemyShield > 0)
        {
            if(damage <= enemyShield)
            {
                enemyShield -= damage;
                damage = 0;
            }
            else
            {
                damage -= enemyShield;
                enemyShield = 0;
            }
        }

        enemyHealth.EnemyTakeDamage(damage);
        ActivateEnemyDamageTaken(damage);
        enemyDamagePopup.CreatePopup(damage.ToString());
        enemyTotalAttack = 0;

        if (enemyHealth.enemyCurrentHealth <= 0)
        {
            foreach (var card in playArea.cardsInPlayArea)
            {
                if (card.isBuffed)
                {
                    card.RevertBuff();
                }
            }

            TurnSystem.Instance.SwitchPhase(CombatPhase.PlayerWin);
        }
    }

    public void HealPlayer(int amount)
    {
        playerHealth.PlayerRegenHealth(amount);
        playerHealthBar.SetHealthBar(playerHealth.playerCurrentHealth);
    }

    public void HealEnemy(int amount)
    {
        enemyHealth.EnemyRegenHealth(amount);
        enemyHealthBar.SetHealthBar(enemyHealth.enemyCurrentHealth);

    }
    private IEnumerator InitializeEnemyHealth()
    {
        yield return new WaitForSeconds(0.001f);
        enemyHealthBar.SetMaxHealthBar(enemyHealth.enemyCurrentHealth, enemyHealth.enemyMaxHealth);

    }

    private void OnPlayerDamageTaken(int damage)
    {
        if (audioManager != null)
        {
            if (damage != 0)
            {
                audioManager.PlaySFX(audioManager.damageTakenSound);
                playerComponent.ShakeObject(0.2f, 50f);
            }
            else
            {
                audioManager.PlaySFX(audioManager.zeroDamageSound);
            }
        }

        playerHealthBar.SetHealthBar(playerHealth.playerCurrentHealth);

    }

    private void OnEnemyDamageTaken(int damage)
    {
        if (audioManager != null)
        {
            if (damage != 0)
            {
                audioManager.PlaySFX(audioManager.damageTakenSound);
                enemyComponent.ShakeObject(0.2f, 50f);
            }
            else
            {
                audioManager.PlaySFX(audioManager.zeroDamageSound);
            }
        }

        enemyHealthBar.SetHealthBar(enemyHealth.enemyCurrentHealth);  
    }

    private void ActivatePlayerDamageTaken(int damage)
    {
        CombatEvents.InvokeOnPlayerDamageTaken(damage);
    }

    private void ActivateEnemyDamageTaken(int damage)
    {
        CombatEvents.InvokeOnEnemyDamageTaken(damage);
    }

    //for testing
    public void Kill()
    {
        int damage = 1000;
        DealDamageToEnemy(damage);
    }
}
