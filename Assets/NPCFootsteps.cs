using UnityEngine;

public class NPCFootsteps : MonoBehaviour
{
    public AudioClip footstepClip;
    public float footstepInterval = 0.5f;

    private float lastFootstepTime = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check if the NPC is moving
        if (rb.velocity.magnitude > 0.1f)
        {
            // Check if enough time has passed since the last footstep
            if (Time.time - lastFootstepTime > footstepInterval)
            {
                // Play the footstep audio clip
                GetComponent<AudioSource>().PlayOneShot(footstepClip);

                // Update the time of the last footstep
                lastFootstepTime = Time.time;
            }
        }
    }
}
