using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private InteractionPopUpController interactionPopUpController;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = new(Screen.width, Screen.height);

        rectTransform.position = Vector3.zero;

    }

    public void DisplayPopUp(string textToDisplay)
    {
        interactionPopUpController.DisplayPopUp(textToDisplay);
    }

    public void StopDisplayingPopUp()
    {
        interactionPopUpController.StopDisplayingPopUp();
    }

}
