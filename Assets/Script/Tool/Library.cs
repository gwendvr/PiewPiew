using UnityEngine;

public class Library : MonoBehaviour
{
    public GameObject[] AllWeapon;

    #region Singleton
    public static Library instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
