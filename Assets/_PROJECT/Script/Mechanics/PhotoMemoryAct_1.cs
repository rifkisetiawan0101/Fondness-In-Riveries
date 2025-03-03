using System.Collections;
using DIALOGUE;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMemoryAct_1 : MonoBehaviour
{
    [SerializeField] private GameObject photoMechanic;
    [SerializeField] private GameObject repairMechanic;
    [SerializeField] private GameObject uiMoveCamera;
    [SerializeField] private GameObject cameraAndHand;
    [SerializeField] private Button buttonOpenCam;
    [SerializeField] private GameObject camViewFinder;
    [SerializeField] private Button buttonPhoto;
    [SerializeField] private Image whiteFlash;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float flashDuration = 0.4f;
    [SerializeField] private Rigidbody2D camRB;
    [SerializeField] private Collider2D babyArellCollider;
    [SerializeField] private Collider2D camCollider;
    [SerializeField] private float smoothingSpeed = 5f;

    private Vector2 camVector;
    private bool isMovingToTarget = false;
    private Vector2 targetPosition;

    private void Start()
    {
        camViewFinder.SetActive(false);
        // Set target position ke local position (0, 0) dari cameraAndHand
        targetPosition = Vector2.zero;
        buttonOpenCam.onClick.AddListener(OpenCam);
    }

    private void Update()
    {
        if (!isMovingToTarget)
        {
            isMovingToTarget = true;
            StartCoroutine(MoveToTargetPosition());
        }

        // Jika kamera dibuka dan belum mengambil foto
        if (MechanicsManager.Instance.isCameraOpened && !MechanicsManager.Instance.isPhotoTaken)
        {
            if (camViewFinder.activeSelf == true)
            {
                CamMovement();
                buttonPhoto.onClick.RemoveAllListeners();
                buttonPhoto.onClick.AddListener(TakePhoto);
            }
        }

        if (MechanicsManager.Instance.isPhotoTaken && !DialogueManager.instance.isRunningConversation && Input.GetKeyDown(KeyCode.Space))
        {
            photoMechanic.SetActive(false);
            repairMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
        }
    }

    private void FixedUpdate()
    {
        if (MechanicsManager.Instance.isCameraOpened && !MechanicsManager.Instance.isPhotoTaken)
        {
            MoveCameraWithinBounds();
        }
    }

    private IEnumerator MoveToTargetPosition()
    {
        while (Vector2.Distance((Vector2)cameraAndHand.transform.localPosition, targetPosition) > 0.1f)
        {
            Vector2 newPosition = Vector2.Lerp((Vector2)cameraAndHand.transform.localPosition, targetPosition, smoothingSpeed * Time.deltaTime);
            cameraAndHand.transform.localPosition = newPosition;
            yield return null;
        }

        // Pastikan posisi benar-benar sesuai target
        cameraAndHand.transform.localPosition = targetPosition;
    }

    private void OpenCam()
    {
        camViewFinder.SetActive(true);
        MechanicsManager.Instance.isCameraOpened = true;
    }

    private void CamMovement()
    {
        camVector = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) camVector.x = -1;
        if (Input.GetKey(KeyCode.D)) camVector.x = 1;
        if (Input.GetKey(KeyCode.W)) camVector.y = 1;
        if (Input.GetKey(KeyCode.S)) camVector.y = -1;

        if (camVector.magnitude > 1) camVector.Normalize();
    }

    private void MoveCameraWithinBounds()
    {
        Vector2 newPosition = camRB.position + camVector * speed * Time.fixedDeltaTime;

        // Hitung batas area (bounds) dari babyArell
        Bounds babyBounds = babyArellCollider.bounds;
        Bounds cameraBounds = camCollider.bounds;

        float offsetX = cameraBounds.extents.x;
        float offsetY = cameraBounds.extents.y;

        // Clamp posisi kamera agar tetap di dalam bounds
        float clampedX = Mathf.Clamp(newPosition.x, babyBounds.min.x + offsetX, babyBounds.max.x - offsetX);
        float clampedY = Mathf.Clamp(newPosition.y, babyBounds.min.y + offsetY, babyBounds.max.y - offsetY);

        camRB.MovePosition(new Vector2(clampedX, clampedY));
    }

    private void TakePhoto()
    {
        if (MechanicsManager.Instance.isPhotoTaken) return;

        uiMoveCamera.SetActive(false);
        MechanicsManager.Instance.isPhotoTaken = true;
        StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        whiteFlash.gameObject.SetActive(true);
        Color flashColor = whiteFlash.color;
        flashColor.a = 0;
        whiteFlash.color = flashColor;

        // Naikkan transparansi
        float elapsed = 0;
        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            flashColor.a = Mathf.Lerp(0, 1, elapsed / (flashDuration / 2));
            whiteFlash.color = flashColor;
            yield return null;
        }

        whiteFlash.color = flashColor;
    }

    // private IEnumerator CloseMechanic()
    // {

    // }
}
