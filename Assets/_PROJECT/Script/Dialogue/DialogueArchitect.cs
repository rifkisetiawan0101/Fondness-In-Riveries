using System.Collections;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueArchitect
    {
        public DialogueArchitect(TextMeshProUGUI tmpro_ui, RectTransform shadowImage) 
        {
            this.tmpro_ui = tmpro_ui;
            this.shadowImage = shadowImage;
        }
        
        public DialogueArchitect(TextMeshPro tmpro_world, RectTransform shadowImage) 
        {
            this.tmpro_world = tmpro_world;
            this.shadowImage = shadowImage;
        }

        private TextMeshProUGUI tmpro_ui;
        private TextMeshPro tmpro_world;
        private RectTransform shadowImage;
        public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

        public string currentText => tmpro.text;
        public string targetText {get; private set;} = "";
        public string preText {get; private set;} = "";
        // private int preTextLength = 0;
        public string fullTargetText => preText + targetText;

        public Color textColor { get {return tmpro.color;} set {tmpro.color = value;}}

        public enum BuildMethod {instant, typewriter}
        public BuildMethod buildMethod = BuildMethod.typewriter;
        public float sizeMultiplier = 1.2f;
        private const float baseSpeed = 1;
        public float speedMultiplier = 0.8f;
        public float shadowPadding = 220f;
        public float textPadding = 80f;
        public float textSpeed
        {
            get { return baseSpeed * speedMultiplier; }
            set { speedMultiplier = value; }
        }

        public int characterPerCycle = 1;
        // private int characterMultiplier = 1;
        // public int characterPerCycle 
        // {
        //     get { return textSpeed <= 2f ? characterMultiplier : textSpeed >= 2f ? characterMultiplier * 2 : characterMultiplier * 3; }
        // }
        
        public Coroutine BuildDialogue(string text) 
        {
            preText = "";
            targetText = text;

            StopBuild();

            buildProcess = tmpro.StartCoroutine(Building());
            return buildProcess;
        }

        // public Coroutine AppendText(string text) 
        // {
        //     preText = tmpro.text;
        //     targetText = text;

        //     StopBuild();

        //     buildProcess = tmpro.StartCoroutine(Building());
        //     return buildProcess;
        // }

        private Coroutine buildProcess = null;
        public bool isBuilding => buildProcess != null;

        public void StopBuild() 
        {
            if (!isBuilding) return;
            tmpro.StopCoroutine(buildProcess);
            buildProcess = null;
        }

        IEnumerator Building()
        {
            Prepare();
            switch(buildMethod)
            {
                case BuildMethod.typewriter:
                    yield return BuildTypewriter();
                    break;
            }
            OnComplete();
        }

        private void OnComplete() 
        {
            buildProcess = null;
        }

        public void ForceComplete()
        {
            switch(buildMethod)
            {
                case BuildMethod.typewriter:
                    tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                    tmpro.ForceMeshUpdate();

                    if (shadowImage != null)
                    {
                        RectTransform textRectTransform = tmpro.GetComponent<RectTransform>();
                        if (textRectTransform != null)
                        {
                            // Get final text size
                            Vector2 textSize = tmpro.GetPreferredValues(tmpro.text);
                            textSize *= sizeMultiplier;

                            // Set final sizes
                            Vector2 sizeDelta = textRectTransform.sizeDelta;
                            sizeDelta.x = textSize.x + textPadding;
                            textRectTransform.sizeDelta = sizeDelta;
                            
                            Vector2 shadowSize = sizeDelta;
                            shadowSize.x += shadowPadding;
                            shadowImage.sizeDelta = shadowSize;
                        }
                    }
                    break;
            }
            StopBuild();
            OnComplete();
        }

        private void Prepare() 
        {
            switch(buildMethod)
            {
                case BuildMethod.instant:
                    PrepareInstant();
                    break;
                case BuildMethod.typewriter:
                    PrepareTypewriter();
                    break;
            }
        }

        private void PrepareInstant()
        {
            tmpro.color = tmpro.color;
            tmpro.text = fullTargetText;
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount; 
            
            if (shadowImage != null)
            {
                Vector2 sizeDelta = shadowImage.sizeDelta;
                sizeDelta.x = 0;
                shadowImage.sizeDelta = sizeDelta;
            }
        }

        private void PrepareTypewriter()
        {
            tmpro.color = tmpro.color;
            tmpro.maxVisibleCharacters = 0;
            tmpro.text = preText;
            // Reset sizes
            if (shadowImage != null)
            {
                RectTransform textRectTransform = tmpro.GetComponent<RectTransform>();
                if (textRectTransform != null)
                {
                    Vector2 sizeDelta = textRectTransform.sizeDelta;
                    sizeDelta.x = 0;
                    textRectTransform.sizeDelta = sizeDelta;

                    shadowImage.sizeDelta = sizeDelta;
                }
            }
            
            tmpro.text += targetText;
            tmpro.ForceMeshUpdate();
        }

        private IEnumerator BuildTypewriter()
        {
            PlayerMovement.Instance.SetTriggerDialogue();
            RectTransform textRectTransform = tmpro.GetComponent<RectTransform>();
            tmpro.ForceMeshUpdate(); // parse full text
            float finalTextWidth = tmpro.GetPreferredValues(tmpro.text).x * sizeMultiplier;

            while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
            {
                tmpro.maxVisibleCharacters += characterPerCycle;
                tmpro.ForceMeshUpdate();
                
                if (shadowImage != null)
                {
                    float progress = (float)tmpro.maxVisibleCharacters / tmpro.textInfo.characterCount;
                    Vector2 currentSize = new Vector2((finalTextWidth * progress) + textPadding, textRectTransform.sizeDelta.y);
                    textRectTransform.sizeDelta = currentSize;

                    Vector2 shadowSize = currentSize;
                    shadowSize.x += shadowPadding;
                    shadowImage.sizeDelta = shadowSize;
                }

                yield return new WaitForSeconds(0.015f / textSpeed);
            }

            // Set final size
            if (shadowImage != null)
            {
                Vector2 finalSize = new Vector2(finalTextWidth + textPadding, textRectTransform.sizeDelta.y);
                textRectTransform.sizeDelta = finalSize;

                Vector2 shadowSize = finalSize;
                shadowSize.x += shadowPadding;
                shadowImage.sizeDelta = shadowSize;
            }
        }
    }
}