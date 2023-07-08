using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpObject : MonoBehaviour
{
    [SerializeField] private float range = 2f;
    [SerializeField] private GameObject hintText;
    public InventoryItem inventoryItem;
    private bool shouldDisplayHint;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(range, 1f);
        hintText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.Instance.OnChangeTarget(this);
            hintText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.Instance.OnChangeTarget(null);
            hintText.SetActive(false);
        }
    }
}
