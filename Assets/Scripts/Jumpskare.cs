using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpScare : MonoBehaviour
{
    [Header("Target Scene")]
    public string sceneName;

    [Header("UI")]
    public GameObject interactText;

    [Header("Jumpscare")]
    public GameObject jumpScareObject;
    public float jumpScareDuration = 1f;

    private bool playerInRange = false;
    private bool hasActivated = false;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        if (jumpScareObject != null)
            jumpScareObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !hasActivated && Input.GetKeyDown(KeyCode.E))
        {
            hasActivated = true;
            StartCoroutine(DoJumpScare());
        }
    }

    IEnumerator DoJumpScare()
    {
        if (jumpScareObject != null)
            jumpScareObject.SetActive(true);

        yield return new WaitForSeconds(jumpScareDuration);

        if (jumpScareObject != null)
            jumpScareObject.SetActive(false);

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactText != null)
                interactText.SetActive(false);
        }
    }
}
