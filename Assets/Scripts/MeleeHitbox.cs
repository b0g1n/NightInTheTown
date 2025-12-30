using UnityEngine;
using System.Collections.Generic;

public class MeleeHitbox : MonoBehaviour
{
    public int damage;
    public float lifetime = 0.3f; // can be longer now

    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only hit bosses
        if (!other.CompareTag("Boss"))
            return;

        // Already hit this target
        if (hitTargets.Contains(other.gameObject))
            return;

        hitTargets.Add(other.gameObject);

        FinalBoss boss = other.GetComponent<FinalBoss>();
        if (boss)
        {
            boss.TakeDamage(damage);
        }
    }
}
