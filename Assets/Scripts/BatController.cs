using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BatController : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 100f;
    private float finalRotation;
    private Rigidbody batterRb;
    private bool isSwingActive = false;
    private bool hasSwung = false;
    [SerializeField] private quaternion initRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        batterRb = GetComponent<Rigidbody>();
        batterRb.centerOfMass = new Vector3(0, 0, 0);
        initRotation = transform.rotation;


    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasSwung)
        {
            //Debug.Log("swing");
            isSwingActive = true;
            hasSwung = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSwingActive)
        {
            batterRb.AddForceAtPosition(new Vector3(swingSpeed, 0, 0), new Vector3(0, 0, 0), ForceMode.Impulse);
            isSwingActive = false;
        }
        if (transform.rotation.eulerAngles.z > 60f)
        {
            //Debug.Log("Stop");
            batterRb.angularVelocity = Vector3.zero;
        }
    }


    public void Reset()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        hasSwung = false;
        isSwingActive = false;
    }

}
