using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HideableObject : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color hiddenColor;
    private Color normalColor;
    private float range = 5f;
    private BoxCollider2D box;
    private CharacterMovement movement;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(range, 1f);
        hintText.SetActive(false);
        normalColor = _renderer.color;
    }

    public void OnHide()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Lamp/Lamp_Possess");
        _renderer.color = hiddenColor;
    }

    public void OnLeave()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Lamp/Lamp_Unpossess");
        _renderer.color = normalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(true);
            if (movement == null) movement = other.GetComponent<CharacterMovement>();

            movement.SetTargetHidableObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(false);
            movement.SetTargetHidableObject(null);
        }
    }
}
