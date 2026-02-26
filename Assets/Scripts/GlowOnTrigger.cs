using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GlowWhenActivated : MonoBehaviour
{
    [Header("Glow Settings")]
    public Renderer objectRenderer;

    [ColorUsage(true, true)]
    public Color glowColor = Color.cyan * 2f;

    [Header("Hand Settings")]
    [Tooltip("The tag assigned to your Right Hand Interactor object")]
    public string rightHandTag = "RightHand";

    private Color originalColor;
    private Material mat;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Automatically grab the XRI component on this object
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (objectRenderer == null) objectRenderer = GetComponent<Renderer>();

        mat = objectRenderer.material;
        mat.EnableKeyword("_EMISSION");
        originalColor = mat.GetColor("_EmissionColor");
    }

    void OnEnable()
    {
        // 'activated' fires when the trigger is pulled while holding the object
        grabInteractable.activated.AddListener(OnTriggerPulled);
        // 'deactivated' fires when the trigger is released
        grabInteractable.deactivated.AddListener(OnTriggerReleased);
        // Turn off glow if we drop the object while the trigger is still pulled
        grabInteractable.selectExited.AddListener(OnDropped);
    }

    void OnDisable()
    {
        grabInteractable.activated.RemoveListener(OnTriggerPulled);
        grabInteractable.deactivated.RemoveListener(OnTriggerReleased);
        grabInteractable.selectExited.RemoveListener(OnDropped);
    }

    private void OnTriggerPulled(ActivateEventArgs args)
    {
        // Check if the interactor holding the object matches our Right Hand tag
        if (args.interactorObject.transform.CompareTag(rightHandTag))
        {
            mat.SetColor("_EmissionColor", glowColor);
        }
    }


    private void OnTriggerReleased(DeactivateEventArgs args)
    {
        mat.SetColor("_EmissionColor", originalColor);
    }

    private void OnDropped(SelectExitEventArgs args)
    {
        mat.SetColor("_EmissionColor", originalColor);
    }
}