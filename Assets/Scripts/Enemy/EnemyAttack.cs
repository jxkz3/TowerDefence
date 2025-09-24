using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 1f;
    public float attackRate = 1f;

    private float attackCooldown = 0f;
    private Wall targetWall;
    private EnemyPath movement;

    void Start()
    {
        movement = GetComponent<EnemyPath>();
    }

    void Update()
    {
        // check if the target wall is gone
        if (targetWall == null)
        {
            if (movement != null) movement.canMove = true; // resume movement
            return;
        }

        // stop movement while attacking
        if (movement != null) movement.canMove = false;

        // attack
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            if (targetWall != null) // extra safety
                targetWall.TakeDamage(damage);
            attackCooldown = 1f / attackRate;
        }

        // if wall destroyed, reset targetWall so movement resumes
        if (targetWall != null && targetWall.Health <= 0)
        {
            targetWall = null;
            if (movement != null) movement.canMove = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (wall != null)
        {
            targetWall = wall;
        }
    }
}
