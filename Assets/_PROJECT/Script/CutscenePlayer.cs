using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    [Header("-- Cutscene Prologue --")]
    public GameObject cutscene1_Prologue;
    [SerializeField] private VideoPlayer cutscene1_VideoPlayer;
    [SerializeField] private float fadeOutDuration = 1.0f;

    [Header("-- Cutscene Prologue --")]
    public GameObject cutscene;
    public Image cutsceneImage;
    public Sprite[] cutsceneFrames;

    public IEnumerator PlayCutcenePrologue()
    {
        if (cutscene1_Prologue != null && cutscene1_VideoPlayer != null)
        {
            cutscene1_Prologue.SetActive(true);
            cutscene1_VideoPlayer.targetCameraAlpha = 1f;
            cutscene1_VideoPlayer.Play();

            float videoLength = (float)cutscene1_VideoPlayer.length;
            float waitTime = videoLength - fadeOutDuration;

            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);

                // Fade out video
                float startTime = Time.time;
                float endTime = startTime + fadeOutDuration;

                while (Time.time < endTime)
                {
                    float t = (Time.time - startTime) / fadeOutDuration;
                    cutscene1_VideoPlayer.targetCameraAlpha = Mathf.Lerp(1.0f, 0.0f, t);
                    yield return null;
                }

                cutscene1_VideoPlayer.targetCameraAlpha = 0.0f;
                yield return new WaitForSeconds(0.1f);
            }
            cutscene1_Prologue.SetActive(false);
        }
    }

    public void ChangeCutsceneFrame(int frameIndex)
    {
        cutsceneImage.sprite = cutsceneFrames[frameIndex];
    }
}
