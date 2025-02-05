using UnityEngine;

public class S_Enemy : MonoBehaviour
{
    public string world;
    
    // Remove enemy from dictionnary when game object is detroy
    void OnDestroy()
    {
        S_EnemyManager.instance.RemoveEnemyFromUniverse(world, this.gameObject);
    }
}
