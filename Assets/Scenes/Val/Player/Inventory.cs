using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[4];


    //Adds a GameObject to the inventory array (will later be a ScriptableObject)
    public void AddItemToInventory(GameObject item, GameObject prefab)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = prefab;
                Destroy(item);
                break;
            }
        }
    }

}
