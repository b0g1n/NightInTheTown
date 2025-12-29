using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OppStoppa : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Scene")]
    public string sceneToLoad;

    [Header("Catch Effect")]
    public RawImage catchImage;
    public float imageDuration = 1f;

    private Transform player;
    private bool triggered;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null || triggered) return;

        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDir);

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(CatchSequence());
    }

    IEnumerator CatchSequence()
    {
        if (catchImage != null)
             catchImage.gameObject.SetActive(true);
        yield return null;

        yield return new WaitForSecondsRealtime(imageDuration);

        SceneManager.LoadScene(sceneToLoad);
    }

}