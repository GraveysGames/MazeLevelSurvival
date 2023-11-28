using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hud_PlayerHealthController_Script : MonoBehaviour
{

    [SerializeField] private GameObject BackGround_HealthBar;
    [SerializeField] private GameObject ForeGround_HealthBar;
    [SerializeField] private TMP_Text text;

    private RectTransform BackGround_HealthBar_RectTransform;
    private RectTransform ForeGround_HealthBar_RectTransform;

    private void Awake()
    {
        ForeGround_HealthBar_RectTransform = ForeGround_HealthBar.GetComponent<RectTransform>();
        BackGround_HealthBar_RectTransform = BackGround_HealthBar.GetComponent<RectTransform>();
    }

    public void UpdateHealth(int playersMaxHealth, int playersCurrentHealth)
    {
        UpdateDisplay(playersMaxHealth, playersCurrentHealth);
    }

    private void UpdateDisplay(int playersMaxHealth, int playersCurrentHealth)
    {
        ForeGround_HealthBar_RectTransform.sizeDelta = new(playersCurrentHealth * 10, ForeGround_HealthBar_RectTransform.sizeDelta.y);
        BackGround_HealthBar_RectTransform.sizeDelta = new(playersMaxHealth * 10, BackGround_HealthBar_RectTransform.sizeDelta.y);
        text.text = playersCurrentHealth.ToString();
    }
}
