using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public List<GameObject> availableLoots;
    [SerializeField] private Animator OpenAnim;
    public bool isOpen = false;

    // Appelé lorsque le joueur interagit avec le lootbox
    public void Open()
    {
        if (availableLoots.Count > 0 && !isOpen)
        {
            OpenAnim.SetBool("Open",true);
            GameObject randomLoot = availableLoots[Random.Range(0, availableLoots.Count)];
            AwardLoot(randomLoot);
            isOpen = true;
        }
    }

    private void AwardLoot(GameObject loot)
    {
        Vector3 spawnPosition = transform.position + Vector3.down * 1.5f; // Adjust the offset as needed
        Instantiate(loot, spawnPosition, Quaternion.identity);
    }
}