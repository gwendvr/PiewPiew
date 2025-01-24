using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


public class S_PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    private Rigidbody2D m_rb;
    [SerializeField]
    private GameObject m_visual;

    [Header("Cursor")]
    [SerializeField]
    private GameObject m_target;
    [SerializeField]
    private float m_cursorDistance;


    [Header("Movement")]
    private Vector2 m_movementInput;
    [SerializeField]
    private float m_movementSpeed;

    [Header("Rotation")]
    private Vector2 m_rotationInput;
    private bool m_useMousePos;

    [Header("Weapon")]
    private S_Weapon m_weapon;
    private List<S_Weapon> m_weaponOnGround = new List<S_Weapon>();
    [SerializeField]
    private Transform m_hand;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Position
        Vector2 _pos = new Vector2(m_rb.position.x + (m_movementInput.x * m_movementSpeed), m_rb.position.y + (m_movementInput.y * m_movementSpeed));
        m_rb.MovePosition(_pos);
        #endregion

        #region Rotation

        #region Rotate with mouse
        Vector2 _lookAtPos = new Vector2(0, 0);
        if (m_useMousePos) 
        {
            Vector3 _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _rotation = _mouseWorldPos - transform.position;
            float _rotZ = Mathf.Atan2(_rotation.y, _rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, _rotZ);
            _mouseWorldPos.z = 0;
            m_target.transform.position = _mouseWorldPos;

        }
        #endregion

        #region Rotate with joystick
        else if (m_rotationInput.x != 0 || m_rotationInput.y != 0)// else use joystick input
        {
            _lookAtPos = new Vector2(transform.position.x + m_rotationInput.x, transform.position.y + m_rotationInput.y);
            float angle = Mathf.Atan2(m_rotationInput.y, m_rotationInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            targetRotation = new Quaternion(targetRotation.x, targetRotation.y, targetRotation.z, targetRotation.w);
            transform.rotation = targetRotation;
            m_target.transform.position = _lookAtPos * m_cursorDistance;
        }
        #endregion

        #endregion
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_movementInput = context.ReadValue<Vector2>(); // Get WASD input normalized
    }
    public void OnRotateMouse(InputAction.CallbackContext context)
    {
        m_useMousePos = true; //Take mouse input
    }

    public void OnRotateJoystick(InputAction.CallbackContext context)
    {
        m_rotationInput = context.ReadValue<Vector2>();
        m_useMousePos = false; // Take joystick input
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Interact");
            if (m_weapon != null) // Remove weapon in hand
            {
                m_weapon.Throw(transform.right);
                m_weapon = null;
            }

            if (m_weaponOnGround.Count > 0) // Take weapon
            {
                m_weapon = m_weaponOnGround[0];
                m_weaponOnGround.Remove(m_weapon);
                m_weapon.transform.parent = m_hand;
                m_weapon.Taken();
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (m_weapon != null) // Attack with Weapon
            {
                m_weapon.Shot(transform.rotation.eulerAngles.z - 90); // Player rotated 90° so re-rotate the Z axis to make the bullet shot forward
            }

            else // Attack CaC with hands
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            m_weaponOnGround.Add(collision.gameObject.GetComponent<S_Weapon>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            m_weaponOnGround.Remove(collision.gameObject.GetComponent<S_Weapon>());
        }
    }
}
