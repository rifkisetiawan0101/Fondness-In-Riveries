using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class DialogueArchitect
{
    public DialogueArchitect(TextMeshProUGUI tmpro_ui) 
    {
        this.tmpro_ui = tmpro_ui;
    }
    public DialogueArchitect(TextMeshPro tmpro_world) 
    {
        this.tmpro_world = tmpro_world;
    }

    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro.text;
    public string targetText {get; private set;} = "";
    public string preText {get; private set;} = "";
    private int preTextLength = 0;
    public string fullTargetText => preText + targetText;

    public Color textColor { get {return tmpro.color;} set {tmpro.color = value;}}

    public enum BuildMethod {instant, typewriter}
    public BuildMethod buildMethod = BuildMethod.typewriter;

    private const float baseSpeed = 1;
    public float speedMultiplier = 0.5f;
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
    
    public Coroutine BuildText(string text) 
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
    public bool isTextBuilding => buildProcess != null;

    public void StopBuild() 
    {
        if (!isTextBuilding) return;
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
    }

    private void PrepareTypewriter() 
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    private IEnumerator BuildTypewriter()
    {
        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += characterPerCycle;
            yield return new WaitForSeconds(0.015f / textSpeed);
        }
    }
}
