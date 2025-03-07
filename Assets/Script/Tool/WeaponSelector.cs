using UnityEngine;
using UnityEditor;

public class WeaponSelector:EditorWindow
{
    private GameObject[] listWeapon;

    [MenuItem("Tools/SelectWeapon")]
    public static void ShowWindow()
    {
        GetWindow<WeaponSelector>("WeaponSelector");
    }

    public void OnEnable()
    {
        listWeapon = Library.instance.AllWeapon;
    }

    public void OnGUI()
    {
        GUILayout.Label("Sélection de l'arme", EditorStyles.boldLabel);
        foreach(GameObject weapon in listWeapon)
        {
            if (GUILayout.Button(weapon.name))
            {
                S_PlayerController player = S_EnemyManager.instance.player.GetComponent<S_PlayerController>();
                S_Weapon currentWeapon = Instantiate(weapon, player.gameObject.transform).GetComponent<S_Weapon>();
                player.m_weapon = currentWeapon;
                player.m_weapon.transform.parent = player.m_hand;
                player.m_weapon.Taken();
            }
        }
    }
}
