using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventory;
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
        inventory = new List<string>();
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

    //Adds a GameObject to the inventory array (will later be a ScriptableObject)
    public void AddItemToInventory(PickUpObject itemToAdd)
    {
        // TODO should not be game object, use SO for rendering UI
        inventory.Add(itemToAdd.gameObject.name);
        Destroy(itemToAdd.gameObject);
    }

}
