using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInfoEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button States")]
    public Sprite defaultSprite;    // Sprite normal/default
    public Sprite hoverSprite;      // Sprite saat hover
    public Sprite activeSprite;     // Sprite saat active/selected
    public Sprite onPressSprite;    // Sprite saat ditekan

    [Header("Button Settings")]
    public bool isActive = false;   // Status apakah button sedang active
    public float pressScale = 0.95f; // Skala button saat ditekan
    public float pressDuration = 0.1f; // Durasi efek tekan (hanya untuk mouse click)

    [Header("Key Binding")]
    public bool useKeyBinding = false; // Apakah menggunakan key binding
    public KeyCode pressKey = KeyCode.None; // Key yang akan memicu onPress

    private Image buttonImage;
    private bool isHovered = false;
    private bool isPressed = false;
    private Vector3 originalScale;
    private float pressTimer = 0f;
    private bool isKeyPressed = false; // Status khusus untuk key press

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ButtonInfoEffect requires an Image component!");
            return;
        }

        // Simpan skala awal
        originalScale = transform.localScale;

        // Set sprite awal
        if (defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    private void Update()
    {
        // Handle efek tekan untuk mouse click
        if (isPressed && !isKeyPressed)
        {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressDuration)
            {
                ResetPressEffect();
            }
        }

        // Handle key binding
        if (useKeyBinding && pressKey != KeyCode.None)
        {
            if (Input.GetKeyDown(pressKey))
            {
                OnKeyPress();
            }
            else if (Input.GetKeyUp(pressKey))
            {
                OnKeyRelease();
            }
        }
    }

    private void OnKeyPress()
    {
        isPressed = true;
        isKeyPressed = true;
        pressTimer = 0f;

        // Terapkan efek tekan
        transform.localScale = originalScale * pressScale;
        if (onPressSprite != null)
        {
            buttonImage.sprite = onPressSprite;
        }

        // Play click sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        }
    }

    private void OnKeyRelease()
    {
        isKeyPressed = false;
        isPressed = false;
        pressTimer = 0f;
        transform.localScale = originalScale;
        
        // Selalu kembali ke default sprite saat key dilepas
        if (defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (!isPressed && !isKeyPressed)
        {
            if (hoverSprite != null)
            {
                buttonImage.sprite = hoverSprite;
                // Play hover sound
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonHover);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (!isPressed && !isKeyPressed)
        {
            if (defaultSprite != null)
            {
                buttonImage.sprite = defaultSprite;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!useKeyBinding) // Hanya terapkan jika tidak menggunakan key binding
        {
            isPressed = true;
            pressTimer = 0f;

            // Terapkan efek tekan
            transform.localScale = originalScale * pressScale;
            if (onPressSprite != null)
            {
                buttonImage.sprite = onPressSprite;
            }

            // Play click sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!useKeyBinding) // Hanya terapkan jika tidak menggunakan key binding
        {
            ResetPressEffect();
        }
    }

    private void ResetPressEffect()
    {
        isPressed = false;
        pressTimer = 0f;
        transform.localScale = originalScale;
        
        if (isHovered && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
        else if (defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    // Method untuk mengubah status active button
    public void SetActive(bool active)
    {
        isActive = active;
        if (active && activeSprite != null)
        {
            buttonImage.sprite = activeSprite;
        }
        else if (defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    // Method untuk toggle status active button
    public void ToggleActive()
    {
        isActive = !isActive;
        if (isActive && activeSprite != null)
        {
            buttonImage.sprite = activeSprite;
        }
        else if (defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
} 