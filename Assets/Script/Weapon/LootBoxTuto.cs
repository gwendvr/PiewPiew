using System.Collections.Generic;
using UnityEngine;

public class LootBoxTuto : MonoBehaviour
{
    public List<GameObject> availableLoots;
    [SerializeField] private Animator OpenAnim;

    // Appelé lorsque le joueur interagit avec le lootbox
    public void Open()
    {
        if (availableLoots.Count > 0)
        {
            OpenAnim.SetBool("Open",true);
            GameObject randomLoot = availableLoots[Random.Range(0, availableLoots.Count)];
            AwardLoot(randomLoot);
        }
    }

    private void AwardLoot(GameObject loot)
    {
        Vector3 spawnPosition = transform.position + Vector3.down * 1.5f; // Adjust the offset as needed
        Instantiate(loot, spawnPosition, Quaternion.identity);
    }
}