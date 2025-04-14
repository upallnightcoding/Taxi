using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaxiCntrl : MonoBehaviour
{
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;

    [SerializeField] private GameObject ammoPrefab;

    private Vector2 playerMove;

    public float flySpeed = 20.0f;
    public float yawAmount = 120.0f;

    private float yaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;

        float horizontalInput = playerMove.x;
        float verticalInput = playerMove.y;

        yaw += horizontalInput * yawAmount * Time.deltaTime;
        //yaw = Mathf.Clamp(yaw, -90.0f, 90.0f);

        float pitch = Mathf.Lerp(0, 45, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput) * 1.5f;
        float roll = Mathf.Lerp(0, 45, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput) * 1.5f;

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMove = context.ReadValue<Vector2>();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject left = Instantiate(ammoPrefab, firePointLeft.position, transform.rotation);
            left.GetComponent<Rigidbody>().AddForce(firePointLeft.transform.forward * 100.0f, ForceMode.Impulse);
            Destroy(left, 2.0f);

            GameObject right = Instantiate(ammoPrefab, firePointRight.position, transform.rotation);
            right.GetComponent<Rigidbody>().AddForce(firePointRight.transform.forward * 100.0f, ForceMode.Impulse);
            Destroy(right, 2.0f);
        }
    }
}
