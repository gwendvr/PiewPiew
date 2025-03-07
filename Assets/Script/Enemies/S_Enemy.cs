using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class S_Enemy : MonoBehaviour
{
    [Header("Enemy Parameters")]
    public string world;
    public float health = 1;

    [Header("Follow Player")]
    protected Transform player;
    protected NavMeshAgent agent;
    public float maxDistance = 4;
    public float minDistance = 1;

    [Header("Path Enemy")]
    public string pathLinked;
    public GameObject[] pathsParent;
    public List<GameObject> paths;
    public int index;

    private void Awake()
    {
        if ((world.Equals("Blue") && !S_DimensionManager.instance.isDimension1)|| (world.Equals("Violet") && S_DimensionManager.instance.isDimension1))
        {
            this.gameObject.SetActive(false);
        }
    }

    protected virtual void Start()
    {
        player = S_EnemyManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        InitializePath();
    }

    protected virtual void Update()
    {
        HandleMovement();
        CheckHealth();
    }

    private void OnDestroy()
    {
        S_EnemyManager.instance.RemoveEnemyFromUniverse(world, this.gameObject);
    }

    protected virtual void InitializePath()
    {
        if (pathLinked == "None")
        {
            return;
        }
        pathsParent = S_EnemyManager.instance.spawnPoints;
        foreach (GameObject path in pathsParent)
        {
            if (path.name == pathLinked)
            {
                var _path = path.GetComponent<S_Path>();
                paths = _path.paths;
            }
        }
        index = 1;
        agent.destination = paths[index].transform.position;
    }

    protected virtual void HandleMovement()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < maxDistance && distancePlayer > minDistance)
        {
            agent.destination = player.position;
        }
        else if (pathLinked == "None")
        {
            return;
        }
        else if (Vector3.Distance(transform.position, agent.destination) <= 0.5)
        {
            index = (index >= paths.Count - 1) ? 0 : index + 1;
            agent.destination = paths[index].transform.position;
        }
    }

    protected virtual void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack"))
        {
            health -= collision.gameObject.GetComponentInParent<S_PlayerController>().GetCirculareDamage();
        }
    }

    public abstract void Attack();
}
