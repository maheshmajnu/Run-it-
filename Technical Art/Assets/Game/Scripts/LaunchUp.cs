using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float launchForce = 10f; // Adjust force as needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the correct tag
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset Y velocity
                rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse); // Apply force
            }
        }
    }
}
