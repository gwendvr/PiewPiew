using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    private Rigidbody2D m_rb;

    [Header("Movement")]
    private Vector2 m_movementInput;
    [SerializeField]
    private float m_movementSpeed;

    [Header("Rotation")]
    private Vector2 m_rotationInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update position
        Vector2 _pos = new Vector2(m_rb.position.x + (m_movementInput.x * m_movementSpeed), m_rb.position.y + (m_movementInput.y * m_movementSpeed));
        m_rb.MovePosition(_pos);

        //Update rotation
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_movementInput = context.ReadValue<Vector2>();
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        m_rotationInput = context.ReadValue<Vector2>();
    }

}
