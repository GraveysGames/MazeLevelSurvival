using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudInteraction : MonoBehaviour
{

    private Canvas canvas;
    [SerializeField] private GameObject playerHudPrefab;

    private GameObject Hud;

    private PlayerHudController hudController;

    private Hud_PlayerHealthController_Script healthBar;

    private CharicterStatsController playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<CharicterStatsController>();

        canvas = MazeEvents.Singleton.canvas;

        Hud = Instantiate(playerHudPrefab, canvas.transform);

        healthBar = Hud.GetComponentInChildren<Hud_PlayerHealthController_Script>();

        hudController = Hud.GetComponent<PlayerHudController>();

        MazeEvents.Singleton.OnUpdateCharicterHealth += UpdateHealth;

    }

    private void OnDestroy()
    {
        MazeEvents.Singleton.OnUpdateCharicterHealth -= UpdateHealth;
    }

    private void UpdateHealth()
    {
        //healthBar.UpdateHealth(playerStats.CurrentMaxHealth, playerStats.CurrentHealth);
    }

    public void DisplayInteraction(string interactionName)
    {
        if (interactionName == null)
        {
            hudController.StopDisplayingPopUp();
        }
        else
        {
            hudController.DisplayPopUp(interactionName);
        }
    }

}
