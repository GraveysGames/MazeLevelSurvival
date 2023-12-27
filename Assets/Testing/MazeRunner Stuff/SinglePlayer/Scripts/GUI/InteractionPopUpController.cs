using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPopUpController : MonoBehaviour
{

    [SerializeField] private TMP_Text text;

    private void Start()
    {
        UserInterfaceEvents.Singleton.OnLookingAtItem += DisplayPopUp;
    }

    public void DisplayPopUp(string textToDisplay)
    {
        if (textToDisplay == null)
        {
            text.text = "";
        }
        else
        {
            text.text = textToDisplay + "\nPress F";
        }
    }

    public void StopDisplayingPopUp()
    {
        text.text = "";
    }

}
