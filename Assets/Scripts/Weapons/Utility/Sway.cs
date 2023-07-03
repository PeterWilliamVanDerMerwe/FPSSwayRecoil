using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Position Sway")]
    [SerializeField] private float intensityPosition;
    [SerializeField] private float smoothPosition;
    [SerializeField] private float maxPosition;

    [Header("Rotation Sway")]
    [SerializeField] private float intensityRotation;
    [SerializeField] private float smoothRotation;
    [SerializeField] private float maxRotation;

    [Header("Movement Sway")]
    [SerializeField] private float intensityMovement;
    [SerializeField] private float smoothMovement;
    [SerializeField] private float maxMovement;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    private float mouseX;
    private float mouseY;
    private Vector3 originPosition;
    private Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.localRotation;
        originPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        SwayUpdate();
    }

    public void SwayUpdate()
    {
        SwayClaculator();
        PositionSway();
        RotationSway();
        WalkSway();
    }

    private void SwayClaculator()
    {
        mouseX = -Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");
    }

    private void PositionSway()
    {
        float targetAdjustmentX = Mathf.Clamp(mouseX * intensityPosition, -maxPosition, maxPosition);
        float targetAdjustmentY = Mathf.Clamp(mouseY * intensityPosition, -maxPosition, maxPosition);

        Vector3 targetPositon = new Vector3(targetAdjustmentX, targetAdjustmentY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPositon + originPosition, Time.deltaTime * smoothPosition);
    }

    private void RotationSway()
    {
        float targetAdjustmentX = Mathf.Clamp(mouseY * intensityRotation, -maxRotation, maxRotation);
        float targetAdjustmentY = Mathf.Clamp(mouseX * intensityRotation, -maxRotation, maxRotation);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(
            rotationX ? -targetAdjustmentX : 0f,
            rotationY ? targetAdjustmentY : 0f,
            rotationZ ? targetAdjustmentY : 0f
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation * originRotation, Time.deltaTime * smoothRotation);
    }

    private void WalkSway()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float targetAdjustmentX = Mathf.Clamp(intensityMovement, -maxMovement, maxMovement);
            float targetAdjustmentY = Mathf.Clamp(intensityMovement, -maxMovement, maxMovement);

            Vector3 targetPositon = new Vector3(-targetAdjustmentX, -targetAdjustmentY, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPositon + originPosition, Time.deltaTime * smoothPosition);
        }
    }
}
