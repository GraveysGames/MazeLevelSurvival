using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterStatsController : MonoBehaviour
{
    [SerializeField] private CharicterStats charicterStats;

    public StatCurrentHealth CurrentHealth { private set; get; }
    public StatBasic MaxHealth { private set; get; }
    public StatHealthRegen HealthRegen { private set; get; }
    public StatBasic PhysicalResistance { private set; get; }
    public StatBasic PhysicalNegation { private set; get; }
    public StatBasic MagicResistance { private set; get; }
    public StatBasic MagicNegation { private set; get; }

    public StatBasic WalkingSpeed { private set; get; }
    public StatBasic RunningSpeed { private set; get; }

    public StatBasic JumpForce { private set; get; }
    public StatBasic WalkingSpeedInAir { private set; get; }
    public StatBasic RunningSpeedInAir { private set; get; }
    public StatBasic WalkingFallSpeed { private set; get; }
    public StatBasic RunningFallSpeed { private set; get; }

    List<CharicterDamageTaken> damageHandlers;

    void Start()
    {
        InitializeStats();
        StatChangeEvents.Current.OnHealthBelowZero += CharicterDeath;
    }

    private void InitializeStats()
    {
        MaxHealth = new(CharicterStats.StatLog.maxHealth, charicterStats.baseMaxHealth);
        CurrentHealth = new( CharicterStats.StatLog.currentHealth, charicterStats.baseStartingHealth, MaxHealth);
        HealthRegen = new(CharicterStats.StatLog.healthRegen, charicterStats.baseHealthRegen, CurrentHealth);
        PhysicalResistance = new(CharicterStats.StatLog.physicalResistance, charicterStats.basePhysicalResistance);
        PhysicalNegation = new(CharicterStats.StatLog.physicalNegation, charicterStats.basePhysicalDamageNegation);
        MagicResistance = new(CharicterStats.StatLog.magicResistance, charicterStats.baseMagicResistance);
        MagicNegation = new(CharicterStats.StatLog.magicNegation, charicterStats.baseMagicDamageNegation);

        WalkingSpeed = new(CharicterStats.StatLog.walkingSpeed, charicterStats.baseWalkingSpeed);
        RunningSpeed = new(CharicterStats.StatLog.runningSpeedMultiplier, charicterStats.baseRunningSpeed);

        JumpForce = new(CharicterStats.StatLog.jumpHeight, charicterStats.baseJumpForce);
        WalkingSpeedInAir = new(CharicterStats.StatLog.walkingSpeedInAir, charicterStats.baseWalkingSpeedInAir);
        RunningSpeedInAir = new(CharicterStats.StatLog.runningSpeedInAir, charicterStats.baseRunningSpeedInAir);
        WalkingFallSpeed = new(CharicterStats.StatLog.walkingFallSpeed, charicterStats.baseWalkingFallSpeed);
        RunningFallSpeed = new(CharicterStats.StatLog.runningFallSpeed, charicterStats.baseRunningFallSpeed);

        SetupDamageHandlers();

    }

    private void SetupDamageHandlers()
    {
        damageHandlers = new();
        damageHandlers.Add(new CharicterDamageTaken(CharicterStats.StatType.PhysicalDamageMitigation, PhysicalResistance, PhysicalNegation, CurrentHealth));
        damageHandlers.Add(new CharicterDamageTaken(CharicterStats.StatType.MagicalDamageMitigation, MagicResistance, MagicNegation, CurrentHealth));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(CharicterStats.StatType.PhysicalDamageMitigation, 10f);
        }
    }

    private void CharicterDeath(float currentHealth)
    {
        Debug.Log("Dead");
    }

    public void TakeDamage(CharicterStats.StatType typeOfDamage, float preMitigationDamageAmount)
    {
        foreach (CharicterDamageTaken damageHandeler in damageHandlers)
        {
            damageHandeler.CalculateDamageTaken(typeOfDamage, preMitigationDamageAmount);
        }
    }

    private void OnDestroy()
    {
        StatChangeEvents.Current.OnHealthBelowZero -= CharicterDeath;
    }
}
