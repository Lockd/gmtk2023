using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotObject;


    public Transform itemSlotsContainer;

    private static InventoryUI _instance;
    public static InventoryUI Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToUI(InventoryItem itemToAdd)
    {
        GameObject itemSlot = Instantiate(itemSlotObject, itemSlotsContainer);
        itemSlot.GetComponent<Image>().sprite = itemToAdd.UIImage;
    }
}
