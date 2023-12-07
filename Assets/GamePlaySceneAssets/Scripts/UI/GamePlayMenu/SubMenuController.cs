using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubMenuController : MonoBehaviour
{

    [SerializeField] private Button backButton;
    [SerializeField] private GameObject backMenuSection;

    private void Start()
    {
        backButton.onClick.AddListener(Back);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        backMenuSection.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameInputs.PauseButton))
        {
            Back();
        }
    }

}
