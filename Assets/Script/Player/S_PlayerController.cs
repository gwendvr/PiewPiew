using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class S_PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    [SerializeField]
    private GameObject m_visual;
    private Rigidbody2D m_rb;
    private float m_currentHealth;
    [SerializeField] private S_PlayerData m_playerData;

    [Space(10)]
    [Header("Cursor")]
    [SerializeField]
    private GameObject m_target;
    [SerializeField]
    private float m_cursorDistance;

    [Space(10)]
    [Header("Movement")]
    private Vector2 m_movementInput;
    private Vector2 m_nextPosition;

    [Space(10)]
    [Header("Rotation")]
    private Vector2 m_rotationInput;
    private bool m_useMousePos;

    [Space(10)]
    [Header("Attack")]
    [SerializeField]
    private float m_cqcDamage; // cqc = Close quarter combat
    [SerializeField]
    private float m_cqcRange;
    [SerializeField]
    private LayerMask m_ennemiesLayer;
    [SerializeField]
    private float m_cqcCouldown;
    private float m_timeSinceLastAttack;
    private bool m_isAttacking;
    [Range(0, 3)]
    [SerializeField]
    private float m_hitPitchMax;
    [Range(0, 3)]
    [SerializeField]
    private float m_hitPitchMin;


    [Space(10)]
    [Header("Weapon")]
    [SerializeField]
    private UnityEngine.Transform m_hand;
    private S_Weapon m_weapon;
    private List<S_Weapon> m_weaponOnGround = new List<S_Weapon>();
    private bool m_isShooting = false;

    [Space(10)]
    [Header("Dimension")]
    [SerializeField]
    private GameObject m_dimensionFilter;
    private bool isDimension1 = true;


    [Space(10)]
    [Header("Dash")]
    [SerializeField]
    private float m_dashStrength;
    private bool m_isDashing;
    private Vector2 m_dashDirection;
    private float m_timeSinceLastDash;
    [SerializeField]
    private float m_dashDuration;
    [SerializeField]
    private GameObject m_dashFXEffect;
    private ParticleSystem m_dashParticleSystem;
    private TrailRenderer m_dashTrailRenderer;
    [SerializeField]
    private RectTransform m_couldownPicture;

    [Space(10)]
    [Header("CirculareAttack")]
    [SerializeField]
    private float m_circulareDamage;
    private bool m_isUsingCirculare;
    private float m_timeSinceLastCirculare;
    [SerializeField]
    private float m_circulareDuration;
    [SerializeField]
    private Animator m_circulareFXEffect;

    [Space(10)]
    [Header("Collectibles")]
    [SerializeField]
    private float m_healByPotion;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentHealth = m_playerData.maxHealth;
        m_rb = GetComponent<Rigidbody2D>();
        m_dashParticleSystem = m_dashFXEffect.GetComponent<ParticleSystem>();
        m_dashTrailRenderer = m_dashFXEffect.GetComponent<TrailRenderer>();
        m_timeSinceLastDash = m_playerData.dashCouldown;
        m_timeSinceLastCirculare = m_playerData.circulareCouldown;
    }

    // Update is called once per frame
    void Update()
    {
        #region Position
        m_nextPosition = new Vector2(m_rb.position.x + (m_movementInput.x * m_playerData.speed), m_rb.position.y + (m_movementInput.y * m_playerData.speed));
        transform.position = m_nextPosition;
        #endregion

        #region Rotation

        #region Rotate with mouse
        Vector2 _lookAtPos = new Vector2(0, 0);
        if (m_useMousePos)
        {
            Vector3 _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _rotation = _mouseWorldPos - transform.position;
            float _rotZ = Mathf.Atan2(_rotation.y, _rotation.x) * Mathf.Rad2Deg;
            Quaternion _rot = Quaternion.Euler(0, 0, _rotZ);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rot, m_playerData.rotationSpeed);
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

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_playerData.rotationSpeed);
            m_target.transform.position = _lookAtPos * m_cursorDistance;
        }
        #endregion

        #endregion

        #region Shot
        if (m_isShooting)
        {
            m_weapon.Shot(transform.rotation.eulerAngles.z - 90); // Player rotated 90° so re-rotate the Z axis to make the bullet shot forward
        }
        #endregion

        #region Attack
        if (m_isAttacking && m_timeSinceLastAttack >= m_cqcCouldown)
        {
            if (Physics2D.Raycast(transform.position, transform.right, m_cqcRange, m_ennemiesLayer))
            {
                GameObject _hit = Physics2D.Raycast(transform.position, transform.right, m_cqcRange, m_ennemiesLayer).collider.gameObject;
                Debug.Log(_hit);
                if (_hit.CompareTag("Ennemy"))
                {
                    _hit.gameObject.GetComponent<S_Enemy>().health -= m_cqcDamage;
                }
            }
            S_AudioManager.instance.PlayAudio("Hit", 0.7f, Random.Range(m_hitPitchMin, m_hitPitchMax));
            m_timeSinceLastAttack = 0;
        }
        else m_timeSinceLastAttack += Time.deltaTime;
        #endregion

        #region Capacity
        #region Dash
        if (m_isDashing)
        {
            m_isDashing = false;
            S_CameraBehaviour.instance.Dash(m_dashDuration);
        }
        else
        {
            m_timeSinceLastDash += Time.deltaTime;
        }
        if (m_timeSinceLastDash < m_playerData.dashCouldown && S_DimensionManager.instance.isDimension1)
        {
            m_couldownPicture.sizeDelta = new Vector2(m_couldownPicture.sizeDelta.x, 100 - (m_timeSinceLastDash * 100) / m_playerData.dashCouldown);
        }
        else if (S_DimensionManager.instance.isDimension1) m_couldownPicture.sizeDelta = new Vector2(m_couldownPicture.sizeDelta.x, 0);
        #endregion

        #region Circulare
        if (m_isUsingCirculare)
        {
            m_isUsingCirculare = false;
        }
        else
        {
            m_timeSinceLastCirculare += Time.deltaTime;
        }
        if (m_timeSinceLastCirculare < m_playerData.circulareCouldown && !S_DimensionManager.instance.isDimension1)
        {
            m_couldownPicture.sizeDelta = new Vector2(m_couldownPicture.sizeDelta.x, 100 - (m_timeSinceLastCirculare * 100) / m_playerData.circulareCouldown);
        }
        else if (!S_DimensionManager.instance.isDimension1) m_couldownPicture.sizeDelta = new Vector2(m_couldownPicture.sizeDelta.x, 0);
        #endregion

        #endregion

        #region Health
        if(m_currentHealth > m_playerData.maxHealth) m_currentHealth = m_playerData.maxHealth;
        #endregion
    }


    #region Input
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
                S_AudioManager.instance.PlayAudio("TakeWeapon");
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (m_weapon != null) // Attack with Weapon
            {
                m_isShooting = true; // LMB pressed
            }

            else // Attack CaC with hands
            {
                m_isAttacking = true;
            }
        }
        else if (context.canceled)
        {
            m_isShooting = false; // LMB released
            m_isAttacking = false;
        }
    }

    public void SwitchDimension(InputAction.CallbackContext context)
    {
        S_DimensionManager.instance.ChangeDimension();
    }

    public void UseCapacity(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (S_DimensionManager.instance.isDimension1 && m_timeSinceLastDash >= m_playerData.dashCouldown) // Use Dash
            {
                m_timeSinceLastDash = 0;
                bool _dashForward = (m_movementInput.x == 0 && m_movementInput.y == 0);
                if (_dashForward)
                {
                    // dash in cursor direction
                    m_dashDirection = transform.right;
                }
                else
                {
                    // dash in walk direction
                    m_dashDirection = m_movementInput;
                }
                m_rb.AddForce(m_dashDirection * m_dashStrength, ForceMode2D.Impulse);
                if (m_dashParticleSystem != null && m_dashTrailRenderer != null)
                {
                    m_dashTrailRenderer.emitting = true;
                    m_dashParticleSystem.Play();
                    m_isDashing = true;
                    StartCoroutine(DisableDashFX());
                }
            }
            else if (!S_DimensionManager.instance.isDimension1) // Use CirculareAttack
            {
                if (m_timeSinceLastCirculare >= m_playerData.circulareCouldown)
                {
                    m_isUsingCirculare = true;
                    m_timeSinceLastCirculare = 0;
                    m_circulareFXEffect.SetTrigger("Use");
                }
            }
        }
    }

    private IEnumerator DisableDashFX()
    {
        yield return new WaitForSeconds(m_dashDuration);
        m_dashParticleSystem.Stop();
        m_dashTrailRenderer.emitting = false;
    }

    #endregion

    #region Health
    public void AddHealth(float _health)
    {
        m_currentHealth += _health;
        if (m_currentHealth > m_playerData.maxHealth) m_currentHealth = m_playerData.maxHealth;
    }

    public void RemoveHealth(float _health)
    {
        m_currentHealth -= _health;
        if (m_currentHealth <= 0) Die();
    }

    private void Die()
    {
        // Dommage... 
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            m_weaponOnGround.Add(collision.gameObject.GetComponent<S_Weapon>());
        }
        if (collision.CompareTag("Potion"))
        {
            AddHealth(m_healByPotion);
            S_AudioManager.instance.PlayAudio("Heal");
            Destroy(collision.gameObject);
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
