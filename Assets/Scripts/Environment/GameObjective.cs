using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Plant, Books, Clothes, Puddle, Dishes, Web }

[RequireComponent(typeof(BoxCollider2D))]
public class GameObjective : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private float timeToComplete = 5f;
    [SerializeField] private float interactionRange = 5F;
    [SerializeField] private InventoryItem requiredItem;
    [SerializeField] private string errorMessage;
    [SerializeField] private string successMessage;
    [SerializeField] private Sprite successImage;
    private BoxCollider2D boxCollider;
    private CharacterMovement movement;
    [SerializeField] private GameObject hintText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool isComplete = false;

    public string type;

    CheckCompletedObjectives completedObjectives;

    Animator playerAnim;

    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>();
        completedObjectives = GameObject.Find("Character").GetComponent<CheckCompletedObjectives>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(interactionRange, 1f);
        hintText.SetActive(false);
    }

    public void OnInteract()
    {
        if (isComplete) return;
        // TODO play correct animaion?
        if (Inventory.Instance.inventory.Contains(requiredItem) || requiredItem == null)
        {
            hintText.SetActive(false);
            playerAnim.transform.parent.gameObject.GetComponent<CharacterMovement>().ChangeMoveAbility(false);
            playerAnim.SetTrigger(type);
            playerAnim.SetBool(type, true);
            PlaySound();
            StartCoroutine(FinishTask());
        }
        else
        {
            if (errorMessage != null) PlayerBubbles.Instance.AddBubble(errorMessage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isComplete)
        {
            if (movement == null) movement = other.GetComponent<CharacterMovement>();

            hintText.SetActive(true);
            movement.SetTargetGameObjective(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(false);
            movement.SetTargetGameObjective(null);
        }
    }

    IEnumerator FinishTask()
    {
        yield return new WaitForSeconds(timeToComplete);
        if (successMessage.Length > 0) PlayerBubbles.Instance.AddBubble(successMessage);
        spriteRenderer.sprite = successImage;
        isComplete = true;
        completedObjectives.CheckGameCompletionStatus();
        playerAnim.SetBool(type, false);

        playerAnim.transform.parent.gameObject.GetComponent<CharacterMovement>().ChangeMoveAbility(true);
    }

    private void PlaySound()
    {
        switch (itemType)
        {
            case ItemType.Plant:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Plants/Plants_Watering");
                break;
            case ItemType.Clothes:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Clothes_Fold");
                break;
            case ItemType.Books:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Books_Organise");
                break;
            case ItemType.Dishes:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Dishes_Clean");
                break;
            case ItemType.Puddle:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Puddle_Clean");
                break;
            case ItemType.Web:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Brush/Brush_Spiderweb");
                break;
            default:
                break;
        }
    }
}
