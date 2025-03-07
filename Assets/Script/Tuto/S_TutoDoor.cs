using UnityEngine;

public class S_TutoDoor : MonoBehaviour
{
    public GameObject tilemap;
    public bool isDoorOpen = false;

    private void Start()
    {
        if (isDoorOpen)
        {
            tilemap.SetActive(false);
        }
        else
        {
            tilemap.SetActive(true);
        }
    }

    public void openDoor()
    {
        tilemap.SetActive(false);
        isDoorOpen = true;
    }

    public void closeDoor()
    {
        tilemap.SetActive(true);
        isDoorOpen = false;
    }
}
