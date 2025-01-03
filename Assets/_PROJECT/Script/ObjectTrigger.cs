using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public enum MechanicName {swingingBaby, toDoList} // isi dengan mekanik lain
    public MechanicName mechanicName = MechanicName.toDoList;
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject mechanics;
    [SerializeField] private bool playerInTrigger = false;
    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
    }

    private void Update() 
    {
        if (playerInTrigger && !mechanicsManager.isOpenMechanic && Input.GetKeyDown(KeyCode.E))
        {
            OpenMechanics();
            ChooseMechanic();
        }
    }

    private void ChooseMechanic()
    {
        switch(mechanicName)
        {
            case MechanicName.toDoList:
                mechanicsManager.isTDLCollected = true;
                mechanicsManager.isOpenMechanic = false;
                gameObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (!mechanicsManager.isOpenMechanic)
            {
                eToInteract.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            eToInteract.SetActive(false);
        }
    }

    private void OpenMechanics()
    {
        eToInteract.SetActive(false);
        mechanics.SetActive(true);
        mechanicsManager.isOpenMechanic = true;
    }
}
