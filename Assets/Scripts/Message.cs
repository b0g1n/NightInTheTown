using UnityEngine;
using TMPro;
using System.Collections;

public class Message : MonoBehaviour
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

    [Header("GameObject")]
    public GameObject ZonaLeave;

    private bool playerInRange;
    private bool busy;

    void Awake()
    {
        
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

        messageText.text = customMessage;
        messageText.gameObject.SetActive(true);

        ZonaLeave.SetActive(true);

        // MESSAGE TIMER (unscaled = cannot randomly cut short)
        float t = 0f;
        while (t < messageDuration)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        messageText.gameObject.SetActive(false);

        // COOLDOWN
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
