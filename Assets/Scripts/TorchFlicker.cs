using UnityEngine;

[RequireComponent(typeof(Light))]
public class TorchFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    public float minIntensity = 5f;  // Set this a bit lower than your normal intensity
    public float maxIntensity = 15f; // Set this a bit higher than your normal intensity

    [Tooltip("Lower numbers = faster flickering")]
    public float flickerSpeed = 0.1f;

    private Light torchLight;
    private float randomOffset;

    void Start()
    {
        torchLight = GetComponent<Light>();
        // Gives each torch a unique flicker pattern if you duplicate them
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // PerlinNoise generates a smooth, organic random wave perfect for fire
        float noise = Mathf.PerlinNoise(Time.time / flickerSpeed, randomOffset);
        torchLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}