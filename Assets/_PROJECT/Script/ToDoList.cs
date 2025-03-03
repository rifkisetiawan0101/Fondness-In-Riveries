using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoList : MonoBehaviour 
{
    [SerializeField] private GameObject tdl;
    public bool isTDLOpen;
    [SerializeField] private GameObject[] taskObjects;
    private LineRenderer[] strikeLines;
    private bool[] isStrikeAnimating;
    private bool[] hasStrikedOut; // Track if strike-through has been completed
    
    [Header("Line Settings")]
    [SerializeField] private float lineWidth = 2f; // Increased default width
    [SerializeField] private float strikeOutDuration = 1f; // Set to 1 second
    [SerializeField] private Color lineColor = Color.black;

    private void Start()
    {
        strikeLines = new LineRenderer[taskObjects.Length];
        isStrikeAnimating = new bool[taskObjects.Length];
        hasStrikedOut = new bool[taskObjects.Length];

        for (int i = 0; i < taskObjects.Length; i++)
        {
            // Create line object as child of task object
            GameObject lineObj = new GameObject($"StrikeLine_{i}");
            lineObj.transform.SetParent(taskObjects[i].transform, false); // Set worldPositionStays to false
            lineObj.transform.localPosition = new Vector3(0, 0, -1f);
            
            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.useWorldSpace = false; // Use local space
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.material = new Material(Shader.Find("Sprites/Default")); // Use sprite shader
            line.startColor = lineColor;
            line.endColor = lineColor;
            line.positionCount = 2;
            
            // Set initial positions in local space
            RectTransform rectTransform = taskObjects[i].GetComponent<RectTransform>();
            float width = rectTransform.rect.width;
            line.SetPosition(0, new Vector3(-width/2, 0, 0));
            line.SetPosition(1, new Vector3(-width/2, 0, 0));
            
            strikeLines[i] = line;
        }
    }

    private void Update()
    {
        if (MechanicsManager.Instance.isTDLCollected && !MechanicsManager.Instance.isOpenMechanic && Input.GetKeyDown(KeyCode.F))
        {
            isTDLOpen = !isTDLOpen;
            tdl.SetActive(isTDLOpen);
            MechanicsManager.Instance.isTDLOpen = isTDLOpen;
        }

        if (MechanicsManager.Instance.isTDLOpen)
        {
            CheckStrikeOut();
        }
    }

    private void CheckStrikeOut()
    {
        if (MechanicsManager.Instance.isMakingMilkPlayed && !hasStrikedOut[0] && !isStrikeAnimating[0])
        {
            StartCoroutine(StrikeOut(0));
        }
        if (MechanicsManager.Instance.isGivingMilkPlayed && !hasStrikedOut[1] && !isStrikeAnimating[1])
        {
            StartCoroutine(StrikeOut(1));
        }
        if (MechanicsManager.Instance.isBathingBabyPlayed && !hasStrikedOut[2] && !isStrikeAnimating[2])
        {
            StartCoroutine(StrikeOut(2));
        }
        if (MechanicsManager.Instance.isRepairSwingPlayed && !hasStrikedOut[3] && !isStrikeAnimating[3])
        {
            StartCoroutine(StrikeOut(3));
        }
    }

    private IEnumerator StrikeOut(int taskIndex)
    {
        if (taskIndex >= taskObjects.Length) yield break;

        isStrikeAnimating[taskIndex] = true;
        LineRenderer line = strikeLines[taskIndex];
        
        // Get local space positions
        RectTransform rectTransform = taskObjects[taskIndex].GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        Vector3 startPos = new Vector3(-width/2, 0, 0);
        Vector3 endPos = new Vector3(width/2, 0, 0);

        // Set initial positions
        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);

        float elapsedTime = 0f;
        
        while (elapsedTime < strikeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / strikeOutDuration;
            
            Vector3 currentEndPos = Vector3.Lerp(startPos, endPos, t);
            line.SetPosition(1, currentEndPos);
            
            yield return null;
        }

        // Ensure final position is exact
        line.SetPosition(1, endPos);
        isStrikeAnimating[taskIndex] = false;
        hasStrikedOut[taskIndex] = true; // Mark as completed
    }
}