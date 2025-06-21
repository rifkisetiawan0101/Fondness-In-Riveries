using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPourBath : MonoBehaviour
{
    [SerializeField] private GameObject objPourWater;
    [SerializeField] private GameObject objBathingBaby;

    // Update is called once per frame
    void Update()
    {
        if (MechanicsManager.Instance.isPourWaterPlayed)
        {
            objBathingBaby.SetActive(true);
            objPourWater.SetActive(false);
        }

        if (MechanicsManager.Instance.isBathingBabyPlayed)
        {
            objBathingBaby.SetActive(false);
            objPourWater.SetActive(false);
        }
    }
}
