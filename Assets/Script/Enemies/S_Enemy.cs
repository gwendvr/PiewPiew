using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    public string world;
    public Transform player;
    public NavMeshAgent agent;

    public float distance;
    void Update()
    {
        //tracker le player
        distance = Vector3.Distance(this.transform.position, player.position);
        if(distance < 4 )
        {
            agent.destination = player.position;
        }
    }

    // Remove enemy from dictionnary when game object is detroy
    void OnDestroy()
    {
        S_EnemyManager.instance.RemoveEnemyFromUniverse(world, this.gameObject);
    }
}
