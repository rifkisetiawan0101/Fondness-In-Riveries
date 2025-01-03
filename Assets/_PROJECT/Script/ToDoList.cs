using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoList : MonoBehaviour
{
    private GameObject fToOpen;
    [SerializeField] private GameObject tdl;
    [SerializeField] private MechanicsManager mechanicsManager;

    private void Start()
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
    }

    private void Update() 
    {
        fToOpen = GameObject.Find("F ToOpen");
        if (mechanicsManager.isTDLCollected && Input.GetKeyDown(KeyCode.F))
        {
            if (fToOpen != null)
            {
                fToOpen.SetActive(false);
            }

            bool isActive = tdl.activeSelf;
            tdl.SetActive(!isActive); // Toggle status
            mechanicsManager.isOpenMechanic = !isActive;
        }
    }
}
