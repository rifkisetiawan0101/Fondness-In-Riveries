using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnEnable : MonoBehaviour
{
    [SerializeField] private GivingMilk givingMilk;
    [SerializeField] private GetWater getWater;
    [SerializeField] private BoilWater boilWater;
    [SerializeField] private BathingBaby bathingBaby;
    public MechanicName mechanicName;

    private void OnEnable()
    {
        StartCoroutine(SetAfterDelay());
    }

    private IEnumerator SetAfterDelay()
    {
        switch (mechanicName)
        {
            case MechanicName.givingMilk:
                yield return new WaitForSeconds(0.5f);
                givingMilk.isMechanicActive = true;
                break;

            case MechanicName.getWater:
                yield return new WaitForSeconds(0.5f);
                getWater.isMechanicActive = true;
                break;

            case MechanicName.boilWater:
                yield return new WaitForSeconds(0.5f);
                boilWater.isMechanicActive = true;
                break;

            case MechanicName.bathingBaby:
                yield return new WaitForSeconds(0.5f);
                bathingBaby.isMechanicActive = true;
                break;

            case MechanicName.none:
                break;
        }
    }

}
