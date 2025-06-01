using UnityEngine;

public class GemPickup : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] public int value = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameManager.score += value;
            gameObject.SetActive(false);
        }
    }
}
