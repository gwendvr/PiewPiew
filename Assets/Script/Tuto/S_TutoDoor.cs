using UnityEngine;

public class S_TutoDoor : MonoBehaviour
{
    public GameObject tilemap;

    private void openDoor(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            tilemap.SetActive(false);
        }
    }
}
