using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        }
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject notifTDLCollected;
    [SerializeField] private bool isNotifTDLPlayed;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
    }

    private void Update() 
    {
        if (mechanicsManager.isTDLCollected && !isNotifTDLPlayed) { StartCoroutine(PlayTDLCollected()); }
    }

    public IEnumerator PlayTDLCollected()
    {
        notifTDLCollected.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notifTDLCollected.SetActive(false);
        isNotifTDLPlayed = true;
        StopCoroutine(PlayTDLCollected());
    }
}
