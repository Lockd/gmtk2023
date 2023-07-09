using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameObjective : MonoBehaviour
{
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

            PlayerBubbles.Instance.ClearBubbles();
        }
    }

    IEnumerator FinishTask()
    {
        yield return new WaitForSeconds(timeToComplete);
        if (successMessage != null) PlayerBubbles.Instance.AddBubble(successMessage);
        spriteRenderer.sprite = successImage;
        isComplete = true;
        completedObjectives.CheckGameCompletionStatus();

        playerAnim.transform.parent.gameObject.GetComponent<CharacterMovement>().ChangeMoveAbility(true);
    }
}
