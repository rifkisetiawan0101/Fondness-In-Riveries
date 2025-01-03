using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsManager : MonoBehaviour
{
    public static MechanicsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        }
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    public bool isTDLCollected;
    public bool isOpenMechanic;
}
