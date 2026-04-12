using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    public BirdMover[] birds;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (BirdMover bird in birds)
            {
                bird.StartMoving();
            }
        }
    }
}