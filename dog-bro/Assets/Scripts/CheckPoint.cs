using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("CheckPoint other name " + other.name);
        // If the player passes through the checkpoint, we activate it
        if (other.name == "Player")
        {
            gameController.resetPosition = gameObject.transform.position;
        }
    }
}
