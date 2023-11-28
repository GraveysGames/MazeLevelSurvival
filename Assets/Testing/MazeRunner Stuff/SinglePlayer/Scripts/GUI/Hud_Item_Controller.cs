using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
public class Hud_Item_Controller : MonoBehaviour
{

    [SerializeField] private GameObject itemDisplayPrefab;
    [SerializeField] private RectTransform itemDisplayContainer;

    private List<GameObject> itemsBeingDisplayed;

    private void Start()
    {
        itemsBeingDisplayed = new();
        GameEvents_Inventory.Current.OnDisplayInventory += DisplayInventory;
    }


    private void DisplayInventory(Dictionary<ItemController.ItemDetails, int> inventory)
    {

        foreach (GameObject item in itemsBeingDisplayed)
        {
            item.SetActive(false);
            Destroy(item);
        }

        itemsBeingDisplayed.Clear();

        foreach (var item in inventory)
        {
            if (item.Key.itemSprite == null)
            {
                Debug.LogWarning("Hud Item has no sprite: " + item.Key.itemStats.name + " (item name)  --Displaying defaults");
            }
            else
            {
                SetupItemDisplay(itemDisplayPrefab, itemDisplayContainer, item.Key.itemSprite, item.Value);
            }
        } 
    }


    private void SetupItemDisplay(GameObject itemDisplayPrefab, RectTransform container, Sprite itemSprite, int ItemCount)
    {

        //Debug.Log("To Display: " + itemSprite + " " + ItemCount + " " + itemDisplayPrefab);

        GameObject newItemToDisplay = Instantiate(itemDisplayPrefab, this.transform);

        newItemToDisplay.GetComponent<Image>().sprite = itemSprite;
        newItemToDisplay.GetComponentInChildren<TMPro.TMP_Text>().text = "" + ItemCount;

        itemsBeingDisplayed.Add(newItemToDisplay);

    }


}
*/