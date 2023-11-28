using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CharicterStatsController : MonoBehaviour
{
    [SerializeField] private CharicterStats baseCharicterStats;

    public int CurrentHealth { get; private set; }
    public int CurrentMaxHealth { get; private set; }
    public int CurrentPhysicalResistance { get; private set; }
    public int CurrentMagicResistance { get; private set; }

    public float CurrentWalkingSpeed { get; private set; }
    public float CurrentRunningSpeed { get; private set; }

    public float CurrentJumpForce { get; private set; }
    public float CurrentWalkingSpeedInAir { get; private set; }
    public float CurrentRunningSpeedInAir { get; private set; }
    public float CurrentWalkingFallSpeed { get; private set; }
    public float CurrentRunningFallSpeed { get; private set; }

    void Start()
    {
        CurrentHealth = baseCharicterStats.baseMaxHealth;

        SetStatsAsBase();


        GameEvents_Inventory.Current.OnDisplayInventory += RecalculateStatsFromItems;
    }

    private void OnDestroy()
    {
        GameEvents_Inventory.Current.OnDisplayInventory -= RecalculateStatsFromItems;
    }

    private void SetStatsAsBase()
    {
        CurrentMaxHealth = baseCharicterStats.baseMaxHealth;
        CurrentPhysicalResistance = baseCharicterStats.basePhysicalResistance;
        CurrentMagicResistance = baseCharicterStats.baseMagicResistance;
        CurrentWalkingSpeed = baseCharicterStats.baseWalkingFallSpeed;
        CurrentRunningSpeed = baseCharicterStats.baseRunningSpeed;
        CurrentJumpForce = baseCharicterStats.baseJumpForce;
        CurrentWalkingSpeedInAir = baseCharicterStats.baseWalkingSpeedInAir;
        CurrentRunningSpeedInAir = baseCharicterStats.baseRunningSpeedInAir;
        CurrentWalkingFallSpeed = baseCharicterStats.baseWalkingFallSpeed;
        CurrentRunningFallSpeed = baseCharicterStats.baseRunningFallSpeed;
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player is Dead");
        }
    }

    private void RecalculateStatsFromItems(Dictionary<ItemController.ItemDetails, int> items)
    {

        foreach (var item in items)
        {
            foreach (CharicterStats.Stats statToChange in item.Key.itemStats.statsEffected)
            {
                FindStatToChange(statToChange, (item.Key, item.Value));
            }
        }
    }

    private void FindStatToChange(CharicterStats.Stats statToEffect, (ItemController.ItemDetails itemDetails, int count) item)
    {
        switch (statToEffect)
        {
            case CharicterStats.Stats.playerName:
                break;
            case CharicterStats.Stats.startingHealth:
                break;
            case CharicterStats.Stats.maxHealth:
                UpdateMaxHealth(item.itemDetails);
                break;
            case CharicterStats.Stats.physicalResistance:
                break;
            case CharicterStats.Stats.magicResistance:
                break;
            case CharicterStats.Stats.walkingSpeed:
                break;
            case CharicterStats.Stats.runningSpeed:
                break;
            case CharicterStats.Stats.jumpForce:
                break;
            case CharicterStats.Stats.walkingSpeedInAir:
                break;
            case CharicterStats.Stats.runningSpeedInAir:
                break;
            case CharicterStats.Stats.walkingFallSpeed:
                break;
            case CharicterStats.Stats.runningFallSpeed:
                break;
        }
    }

    private void UpdateMaxHealth(ItemController.ItemDetails itemDetails)
    {
        CurrentMaxHealth += itemDetails.itemStats.ChangeInMaxHealth;
        CurrentHealth += itemDetails.itemStats.ChangeInMaxHealth;
    }

}
*/