using UnityEngine;

public class S_EnemyRanged : S_Enemy
{
    public S_WeaponData data;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    protected override void HandleMovement()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < maxDistance)
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            agent.isStopped = false;
            base.HandleMovement();
        }
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            S_Bullet _bullet = S_PoolingSystem.instance.GetEnemyBullet();
            _bullet.transform.position = transform.position;
            _bullet.transform.rotation = transform.rotation;

            _bullet.Shot(data);

        }
    }
}
