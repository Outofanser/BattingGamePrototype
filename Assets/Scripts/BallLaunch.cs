using UnityEngine;

public class BallLaunch : MonoBehaviour
{
    private Rigidbody ballRb;
    [SerializeField] public float ballSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.AddForce(Vector3.left * ballSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
