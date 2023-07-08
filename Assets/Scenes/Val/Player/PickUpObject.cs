using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    bool canBePickedUp;
    Inventory playerInventory;

    public GameObject takeText;
    //Prefab GameObject to add to inventory, will later be a scriptable object.
    public GameObject thisObjectPrefab;
    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); 
    }
    // Update is called once per frame
    void Update()
    {
        takeText.SetActive(canBePickedUp);
        //Adds this GameObject to inventory if player is in range and presses "E".
        if(canBePickedUp && Input.GetKeyDown(KeyCode.E))
        {
            playerInventory.AddItemToInventory(gameObject, thisObjectPrefab);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            canBePickedUp = false;
        }
    }
}
