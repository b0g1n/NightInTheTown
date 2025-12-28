using UnityEngine;
using UnityEngine.SceneManagement;

public class OppStoppa : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Scene")]
    public string sceneToLoad;

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

        // Look at player (Y axis only)
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDir);

        // Move toward player
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

        // Load scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
