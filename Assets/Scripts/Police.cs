using UnityEngine;
using TMPro;
using System.Collections;

public class Police : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI messageText;

    [Header("Message")]
    [TextArea]
    public string customMessage;
    public float messageDuration = 5f;

    [Header("Cooldown")]
    public float cooldownTime = 3f;

    [Header("GameObjects")]
    public GameObject zonaLeave;
    public GameObject objectToDisable;
    public GameObject objectToEnable;

    private bool playerInRange;
    private bool busy;
    private bool activated;

    private AudioSource mainCameraAudio;

    void Awake()
    {
        Camera cam = Camera.main;
        if (cam != null)
            mainCameraAudio = cam.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && !busy && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(InteractionFlow());
        }
    }

    IEnumerator InteractionFlow()
    {
        busy = true;

        interactText.gameObject.SetActive(false);

        // Disable main camera audio
        if (mainCameraAudio != null)
            mainCameraAudio.enabled = false;

        // Toggle objects ONCE
        if (!activated)
        {
            if (zonaLeave != null)
                zonaLeave.SetActive(true);

            if (objectToDisable != null)
                objectToDisable.SetActive(false);

            if (objectToEnable != null)
                objectToEnable.SetActive(true);

            activated = true;
        }

        // Show message
        messageText.text = customMessage;
        messageText.gameObject.SetActive(true);

        float t = 0f;
        while (t < messageDuration)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        messageText.gameObject.SetActive(false);

        // Cooldown
        t = 0f;
        while (t < cooldownTime)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        busy = false;

        if (playerInRange)
            interactText.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (!busy)
            interactText.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        interactText.gameObject.SetActive(false);
    }
}
