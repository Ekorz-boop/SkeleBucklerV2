using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwordSwoosh : MonoBehaviour
{
    [Header("Effects")]
    public TrailRenderer trail;
    public AudioSource swooshAudio;

    [Header("Settings")]
    [Tooltip("How fast the sword must move to trigger effects (meters per second)")]
    public float swingThreshold = 3.0f;

    [Tooltip("Time between sounds so it doesn't spam")]
    public float audioCooldown = 0.3f;

    private Rigidbody rb;
    private float lastSwooshTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Auto-grab the components if you forgot to drag them in
        if (trail == null) trail = GetComponent<TrailRenderer>();
        if (swooshAudio == null) swooshAudio = GetComponent<AudioSource>();

        if (trail != null) trail.emitting = false;
    }

    void Update()
    {
        // In Unity 6, linearVelocity is the standard for checking physics speed
        float currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed > swingThreshold)
        {
            // Turn on the visual trail
            if (trail != null) trail.emitting = true;

            // Play the sound if enough time has passed
            if (swooshAudio != null && Time.time > lastSwooshTime + audioCooldown)
            {
                // Slightly randomize the pitch so consecutive swings don't sound identical
                swooshAudio.pitch = Random.Range(0.9f, 1.3f);
                swooshAudio.Play();

                lastSwooshTime = Time.time;
            }
        }
        else
        {
            // Turn off the trail when moving slowly
            if (trail != null) trail.emitting = false;
        }
    }
}