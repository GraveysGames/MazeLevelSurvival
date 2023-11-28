using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPopUpController : MonoBehaviour
{

    [SerializeField] private TMP_Text text;

    public void DisplayPopUp(string textToDisplay)
    {
        text.text = textToDisplay + "\nPress F";
    }

    public void StopDisplayingPopUp()
    {
        text.text = "";
    }

}
