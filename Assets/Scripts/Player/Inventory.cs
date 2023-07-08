using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory;
    [SerializeField] private PickUpObject targetObject;

    private static Inventory _instance;
    public static Inventory Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
    }

    public void OnChangeTarget(PickUpObject pickUpObject)
    {
        targetObject = pickUpObject;
    }

    public void OnPickUp()
    {
        if (targetObject == null) return;

        AddItemToInventory(targetObject);
    }

    //Adds a InventoryItem to the inventory list
    public void AddItemToInventory(PickUpObject itemToAdd)
    {
        // TODO should not be game object, use SO for rendering UI
        inventory.Add(itemToAdd.inventoryItem);
        InventoryUI.Instance.AddItemToUI(itemToAdd.inventoryItem);
        Destroy(itemToAdd.gameObject);
    }

}
