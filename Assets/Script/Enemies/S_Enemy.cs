using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    [Header("Enemy parameters")]
    public string world;
    public float health = 1;

    [Header("follow Player")]
    public Transform player;
    public NavMeshAgent agent;
    public float maxDistance = 4;
    public float minDistance = 1;
    public float distancePlayer;

    [Header("Path Enemy")]
    public string pathLinked;
    public GameObject[] pathsParent;
    public List<GameObject> paths;
    public int index;

    private void Start()
    {
        if (player == null)
        {
            player = S_EnemyManager.instance.player.transform;
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

    void Update()
    {
        // tracker le player
        distancePlayer = Vector3.Distance(this.transform.position, player.position);
        if(distancePlayer < maxDistance )
        {
            if (distancePlayer > minDistance)
            {
                agent.destination = player.position;
            }
        }
        else
        {
            if(Vector3.Distance(this.transform.position, agent.destination) <= 0.5)
            {
                if (index >= paths.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                agent.destination = paths[index].transform.position;
            }
        }

        if (health <= 0 )
        {
            Destroy(this.gameObject);
        }
    }

    // Remove enemy from dictionnary when game object is detroy
    void OnDestroy()
    {
        S_EnemyManager.instance.RemoveEnemyFromUniverse(world, this.gameObject);
    }
}
