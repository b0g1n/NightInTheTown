using UnityEngine;
using TMPro;

public class FinalBoss : MonoBehaviour
{
    public int maxHP = 200;
    private int currentHP;

    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int damageToPlayer = 20;
    public float attackCooldown = 1.5f;

    public Transform player;
    public GameObject deadBodyPrefab;
    public AudioSource defeatSound;

    public TMP_Text victoryText;
    public TMP_Text bossHpText;

    private float nextAttackTime;

    void Start()
    {
        currentHP = maxHP;
        UpdateBossHpText();
    }

    void Update()
    {
        if (!player) return;

        // Face player
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

        // Move toward player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            player.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateBossHpText();

        if (currentHP <= 0)
            Die();
    }

    void UpdateBossHpText()
    {
        if (bossHpText)
            bossHpText.text = currentHP + " / " + maxHP;
    }

    void Die()
    {
        if (defeatSound) defeatSound.Play();

        // Spawn dead body prefab
        GameObject deadBody = Instantiate(deadBodyPrefab, transform.position, Quaternion.Euler(-91f, 0f, 0f));

        // Trigger fade + scene change if dead body has the script
        FadeAndSceneChanger fader = deadBody.GetComponent<FadeAndSceneChanger>();
        if (fader != null)
            fader.StartFade();

        // Hide boss UI
        if (bossHpText)
            bossHpText.gameObject.SetActive(false);

        if (victoryText)
        {
            victoryText.gameObject.SetActive(true);
            victoryText.text = "I did it...";
        }

        // Destroy boss
        Destroy(gameObject);
    }
}
