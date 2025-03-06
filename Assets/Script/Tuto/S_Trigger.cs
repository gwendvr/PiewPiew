using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class S_Trigger : MonoBehaviour
{
    [SerializeField, TagMaskField] private string tagsInteractible;
    public UnityEvent triggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagsInteractible == collision.gameObject.tag)
        {
            triggerEvent.Invoke();
        }
    }
}
