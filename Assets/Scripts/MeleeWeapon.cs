using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public Transform hitboxSpawnPoint;

    public int damage = 25;
    public float attackCooldown = 1f;

    public Transform weaponModel;
    public float attackMoveDistance = 0.3f;
    public float attackSpeed = 10f;

    private float nextAttackTime;
    private Vector3 weaponStartPos;
    private bool attacking;

    void Start()
    {
        if (weaponModel)
            weaponStartPos = weaponModel.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
        }

        AnimateWeapon();
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;

        // Spawn melee hitbox
        GameObject hitbox = Instantiate(
            hitboxPrefab,
            hitboxSpawnPoint.position,
            hitboxSpawnPoint.rotation
        );

        MeleeHitbox mh = hitbox.GetComponent<MeleeHitbox>();
        if (mh)
            mh.damage = damage;

        attacking = true;
    }

    void AnimateWeapon()
    {
        if (!weaponModel) return;

        if (attacking)
        {
            weaponModel.localPosition = Vector3.MoveTowards(
                weaponModel.localPosition,
                weaponStartPos + Vector3.down * attackMoveDistance,
                attackSpeed * Time.deltaTime
            );

            if (Vector3.Distance(
                weaponModel.localPosition,
                weaponStartPos + Vector3.down * attackMoveDistance) < 0.01f)
            {
                attacking = false;
            }
        }
        else
        {
            weaponModel.localPosition = Vector3.MoveTowards(
                weaponModel.localPosition,
                weaponStartPos,
                attackSpeed * Time.deltaTime
            );
        }
    }
}
