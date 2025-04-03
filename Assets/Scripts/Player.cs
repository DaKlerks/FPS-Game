using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1;
    public float rotationSpeed = 1;
    public new GameObject camera;
    private Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;
    private float MouseInputX;
    private float MouseInputY;
    float cameraYRot;
    float cameraXRot;

    [Header("Health")]
    [SerializeField] private Slider slider;
    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMovement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        rb.AddForce(transform.forward * Time.deltaTime * verticalInput * speed, ForceMode.Impulse);
        rb.AddForce(transform.right * Time.deltaTime * horizontalInput * speed, ForceMode.Impulse);

        //CameraMovement
        MouseInputX = Input.GetAxis("Mouse X");
        MouseInputY = Input.GetAxis("Mouse Y");

        cameraYRot -= MouseInputY * rotationSpeed; 
        cameraYRot = Mathf.Clamp(cameraYRot, -90f, 90f);

        cameraXRot = MouseInputX * rotationSpeed;

        camera.transform.localEulerAngles = Vector3.right * cameraYRot;
        transform.Rotate(Vector3.up * cameraXRot);

        slider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //game over;
            Debug.Log("You Died!");
        }
    }
}
