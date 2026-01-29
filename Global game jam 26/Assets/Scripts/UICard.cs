using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class UICard : MonoBehaviour
{
    [SerializeField] float savedIndex;
    [SerializeField] float orbitSpeed;
    [SerializeField] float rotationAmount;
    private Camera mainCamera;

    RectTransform rectTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

        float orbitAngle = (Time.time * orbitSpeed) + savedIndex;

        // Compute subtle tilt/wobble using the same phase
        float sine = Mathf.Sin(orbitAngle);
        float cosine = Mathf.Cos(orbitAngle);

        rectTransform.localRotation = Quaternion.Euler(sine * rotationAmount, cosine * rotationAmount, 0);

    }
}
