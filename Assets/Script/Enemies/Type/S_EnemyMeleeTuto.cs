using UnityEngine;

public class S_EnemyMeleeTuto : S_Enemy
{
    public float attackCooldown = 1.5f;
    private float lastAttackTime;
    public float damage;
    public float range;
    public LayerMask playerLayer;

    protected override void HandleMovement()
    {
        base.HandleMovement();
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            
            if (Physics2D.Raycast(transform.position, transform.right, range, playerLayer))
            {
                GameObject _hit = Physics2D.Raycast(transform.position, transform.right, range, playerLayer).collider.gameObject;
                Debug.Log(_hit);
                if (_hit.CompareTag("Ennemy"))
                {
                    //_hit.GetComponent<S_PlayerController>().RemoveHealth(damage);
                }
            }
        }
    }

    protected override void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            S_Tuto.instance.EnnemyDead();
        }
    }
}
