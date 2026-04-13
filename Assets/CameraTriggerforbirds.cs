using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.name == "player")
        {
            Debug.Log("Camera entered trigger → birds moving!");

            BirdMover[] birds = FindObjectsOfType<BirdMover>();

            foreach (BirdMover bird in birds)
            {
                bird.StartMoving();
            }
        }
    }
}