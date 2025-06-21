using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Sprites")]
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite clickedSprite;

    [Header("Click Effect Settings")]
    public float clickScale = 0.95f; // Skala button saat diklik
    public Color clickColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Warna button saat diklik

    private Image buttonImage;
    private bool isHovered = false;
    private bool isClicked = false;
    private Vector3 originalScale;
    private Color originalColor;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ButtonHoverEffect requires an Image component!");
            return;
        }

        // Simpan nilai awal
        originalScale = transform.localScale;
        originalColor = buttonImage.color;

        // Set sprite awal
        if (normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (!isClicked && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
            // Play hover sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonHover);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (!isClicked && normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        // Terapkan efek klik
        transform.localScale = originalScale * clickScale;
        buttonImage.color = clickColor;

        if (clickedSprite != null)
        {
            buttonImage.sprite = clickedSprite;
        }

        // Play click sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
        transform.localScale = originalScale;
        buttonImage.color = originalColor;

        // Kembali ke sprite yang sesuai
        if (isHovered && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
        else if (normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }

    // Method untuk mereset status hover dan sprite ke default
    public void ResetToDefault()
    {
        isHovered = false;
        isClicked = false;
        transform.localScale = originalScale;
        buttonImage.color = originalColor;
        if (normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }
} 