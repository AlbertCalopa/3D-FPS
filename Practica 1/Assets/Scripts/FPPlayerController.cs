using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPPlayerController : MonoBehaviour
{

    float m_Yaw;
    float m_Pitch;
    public float m_YawRotationSpeed;
    public float m_PitchRotationSpeed;
    public float m_MinPitch;
    public float m_MaxPitch;

    public Transform m_PitchController;

    public bool m_UseYawInverted;
    public bool m_UsePitchInverted;
    

    public CharacterController m_CharacterController;
    public float m_PlayerSpeed;
    public float m_FastSpeedMultiplier;
    public KeyCode m_LeftKeyCode;
    public KeyCode m_RightKeyCode;
    public KeyCode m_UpKeyCode;
    public KeyCode m_DownKeyCode;
    public KeyCode m_JumpKeyCode = KeyCode.Space;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;
    public KeyCode m_DebugLockAngleCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    bool m_AngleLocked = false;
    bool m_AimLocked = true;

    public Camera m_Camera;
    public float m_NormalMovementFOV = 60.0f;
    public float m_RunMovementFOV = 75.0f;
    public float m_FOVSpeed = 0.5f;

    float m_VerticalSpeed = 0.0f;
    bool m_OnGround = true;

    public float m_JumpSpeed = 10.0f;

    float m_TimeOfGround;
    public float m_TimeGrounded = 0.2f;

    public float m_MaxShootDistance;

    public LayerMask m_ShootingLayerMask;

    public GameObject m_DecalPrefab;
    TCObjectPool m_DecalsPool;

    [Header("Animations")]
    public Animation m_Animation;
    public AnimationClip m_IdleAnimationClip;
    public AnimationClip m_ShootAnimationClip;
    public AnimationClip m_ReloadAnimationClip;

    bool m_Shooting = false;

    Vector3 m_StartPosition;
    Quaternion m_StartRotation;

    public float m_Health; //el profe el te com life
    public float m_bullets = 10;
    public float m_MaxBullets = 10;
     
    public float m_Shield;

   


    void Start()
    {
        m_Health = GameController.GetGameController().GetPlayerHealth();
        m_Shield = GameController.GetGameController().GetPlayerShield();
        GameController.GetGameController().SetPlayer(this);
        Debug.Log("Health: " + m_Health);
        m_Yaw = transform.rotation.y;
        m_Pitch = m_PitchController.localRotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        //#if UNITY_EDITOR //para poder bloquear o desbloquear cosas cuando le das a play
        //#else
        //#endif

        SetIdleWeaponAnimation();
        m_StartPosition = transform.position;
        m_StartRotation = transform.rotation;

        m_DecalsPool = new TCObjectPool(10, m_DecalPrefab);

    }
#if UNITY_EDITOR
    void UpdateInputDebug()
    {
        if (Input.GetKeyDown(m_DebugLockAngleCode))
        {
            m_AngleLocked = !m_AngleLocked;
        }
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
            }
        }
    }
#endif

    void Update()
    {
        UpdateInputDebug();
        //Debug.Log("Health: " + m_Health);

        m_TimeOfGround += Time.deltaTime;
        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        Vector3 l_Direction = Vector3.zero;

        float l_Speed = m_PlayerSpeed;

        if (Input.GetKey(m_UpKeyCode))
            l_Direction = l_ForwardDirection;
        if (Input.GetKey(m_DownKeyCode))
            l_Direction -= l_ForwardDirection;
        if (Input.GetKey(m_RightKeyCode))
            l_Direction += l_RightDirection;
        if (Input.GetKey(m_LeftKeyCode))
            l_Direction -= l_RightDirection;
        if(Input.GetKeyDown(m_JumpKeyCode) && m_OnGround)
        {
            m_VerticalSpeed = m_JumpSpeed;
        }
        float l_FOV = m_NormalMovementFOV;
        if (Input.GetKey(m_RunKeyCode))
        {
            l_Speed = m_PlayerSpeed * m_FastSpeedMultiplier;
            if (l_Direction != Vector3.zero)
            {
                l_FOV = Mathf.Lerp(m_Camera.fieldOfView, m_RunMovementFOV, m_FOVSpeed * Time.deltaTime);
            }
            
            //l_FOV = m_RunMovementFOV;
        }
        m_Camera.fieldOfView = l_FOV;

        l_Direction.Normalize();

        Vector3 l_movement = l_Direction * l_Speed * Time.deltaTime;

        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");
#if UNITY_EDITOR
        if (m_AngleLocked)
        {
            l_MouseX = 0.0f;
            l_MouseY = 0.0f;
        }
#endif

        m_Yaw = m_Yaw + l_MouseX * m_YawRotationSpeed * Time.deltaTime * (m_UseYawInverted ? -1.0f : 1.0f); //(m_UseYawInverted ? -1.0f : 1.0f) Si el bool es correcte fara lo primer, sinó lo segon.
        m_Pitch = m_Pitch + l_MouseY * m_PitchRotationSpeed * Time.deltaTime * (m_UsePitchInverted ? -1.0f : 1.0f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch); //Mathf.Clamp et clava el valor al minim o maxim que haguis donat o al de la meitat.

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);

        m_VerticalSpeed = m_VerticalSpeed + Physics.gravity.y * Time.deltaTime;
        l_movement.y = m_VerticalSpeed * Time.deltaTime;

        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_movement);
        if((l_CollisionFlags & CollisionFlags.Above)!= 0 && m_VerticalSpeed > 0.0f)
        {
            m_VerticalSpeed = 0.0f;
        }
        if((l_CollisionFlags & CollisionFlags.Below)!= 0)
        {
            m_VerticalSpeed = 0.0f;
            m_OnGround = true;
            m_TimeOfGround = 0;
        }
        else
        {
            m_OnGround = false;
        }
        if(m_TimeOfGround < m_TimeGrounded)
        {
            m_OnGround = true;
        }

        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
        }        
        

        bool CanShoot()
        {
            return true;
        }
        
        void Shoot()
        {
            m_bullets--;
            Ray l_Ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit l_RaycastHit;
            if(Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxShootDistance, m_ShootingLayerMask.value))
            {
                if(l_RaycastHit.collider.tag == "DroneCollider")
                {
                    l_RaycastHit.collider.GetComponent<HitCollider>().Hit();
                }
                CreateShootHitParticles(l_RaycastHit.collider, l_RaycastHit.point, l_RaycastHit.normal);
            }
            SetShootWeaponAnimation();
            m_Shooting = true;

        }
        void CreateShootHitParticles(Collider _Collider, Vector3 Position, Vector3 Normal)
        {
            //Debug.DrawRay(Position, Normal * 5.0f, Color.red, 2.0f);
            //GameObject.Instantiate(m_DecalPrefab, Position, Quaternion.LookRotation(Normal));
            GameObject l_Decal = m_DecalsPool.GetNextElement();
            l_Decal.SetActive(true);
            l_Decal.transform.position = Position;
            l_Decal.transform.rotation = Quaternion.LookRotation(Normal);
        }

        
    }
    void SetIdleWeaponAnimation()
    {
        m_Animation.CrossFade(m_IdleAnimationClip.name);
    }
    void SetShootWeaponAnimation()
    {
        m_Animation.CrossFade(m_ShootAnimationClip.name, 0.1f);
        m_Animation.CrossFadeQueued(m_IdleAnimationClip.name, 0.1f);
        StartCoroutine(EndShoot());
    }

    IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(m_ShootAnimationClip.length);
        m_Shooting = false;
    }

    public float getLife() //devuelve la vida del pj
    {
        return m_Health;
    }
    public void AddLife(float Life) //añades la vida al pj
    {
        m_Health = Mathf.Clamp(m_Health + Life, 0.0f, 1.0f);
        //Debug.Log(m_Health);
    }
    
    public float getShield()
    {
        return m_Shield;
    }

    public void AddShield(float shield)
    {
        m_Shield = Mathf.Clamp(m_Shield + shield, 0.0f, 1.0f);
    }

    public void OnTriggerEnter(Collider other) //si colisiona
    {
        if(other.tag == "Item") //con "item"
        {
            other.GetComponent<Item>().Pick(this); //lo coge
        }
        else if (other.tag == "DeadZone")
        {
            Kill();
        }
    }

  



    void Kill()
    {
        m_Health = 0.0f;
        GameController.GetGameController().RestartGame();

    }
    public void RestartGame() 
    {
        m_Health = 1.0f;
        m_CharacterController.enabled = false;
        transform.position = m_StartPosition;
        transform.rotation = m_StartRotation;
        m_CharacterController.enabled = true;

    }
}
