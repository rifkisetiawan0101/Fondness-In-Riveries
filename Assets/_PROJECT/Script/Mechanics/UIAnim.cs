using UnityEngine;
using UnityEngine.UI;

public class UIAnim : MonoBehaviour
{
    // Public array for frame animations (sprites)
    public Sprite[] frames;

    // Animation frame rate (12 FPS)
    public float frameRate = 12f;

    // Reference to the UnityEngine.UI.Image component
    Image targetImage;

    private int currentFrame = 0;
    private bool isPlaying = false;
    private float timer = 0f;
    
    public bool isStartNull = false;
    public bool isLooping = true;

    void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    void Update()
    {
        // image is invisible if no sprite is set
        Color color = targetImage.color;
        color.a = (targetImage.sprite != null) ? 1f : 0f;
        targetImage.color = color;

        if (isPlaying && frames != null && frames.Length > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / frameRate)
            {
                timer -= 1f / frameRate;
                targetImage.sprite = frames[currentFrame];
                if (isLooping)
                {
                    currentFrame = (currentFrame + 1) % frames.Length;
                }
                else
                {
                    if (currentFrame < frames.Length - 1)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        isPlaying = false;
                    }
                }
            }
        }
    }

    // Public function to play animation
    public void PlayAnimation()
    {
        isPlaying = true;
    }

    // Public function to pause animation
    public void PauseAnimation()
    {
        isPlaying = false;
    }

    // Public function to reset animation
    public void ResetAnimation()
    {
        isPlaying = false;
        currentFrame = 0;
        timer = 0f;
        if (targetImage != null)
        {
            if (isStartNull)
            {
                targetImage.sprite = null;
            }
            else if (frames != null && frames.Length > 0)
            {
                targetImage.sprite = frames[0];
            }
        }
    }
}
