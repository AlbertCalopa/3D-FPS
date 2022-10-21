using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DronEnemy2 : MonoBehaviour
{
    public enum TState
    {
        IDLE = 0,
        PATROL,
        ALERT,
        CHASE,
        ATTACK,
        HIT,
        DIE
    }

    NavMeshAgent NavMeshAgent;
    public List<Transform> PatrolList;    
    Queue<Transform> PatrolQueue = new Queue<Transform>();

    [SerializeField]
    float HearRangeDistance;
    float VisualConeAngle = 60.0f;
    [SerializeField]
    float SightDistance = 8.0f;
    float EyesHeight = 0.0f;
    float EyesPlayerHeight = 1.8f;
    float RotateTimer = 0.0f;

    LayerMask SightLayerMask;
    [SerializeField]
    TState _State;

    public Image m_LifeBarImage;
    public Transform m_LifeBarAnchorPosition;
    public RectTransform m_LifeBarRectTransform;
    float m_Life = 1.0f;
    public TState State
    {
        get { return _State; }
        set
        {
            SetterState(value);
            _State = value;
        }
    }

    void SetterState(TState state)
    {
        NavMeshAgent.isStopped = true;
        switch (state)
        {
            case TState.IDLE:
                break;
            case TState.PATROL:
                NavMeshAgent.isStopped = false;
                NavMeshAgent.SetDestination(PatrolQueue.Peek().position);
                break;
            case TState.ALERT:
                RotateTimer = 6.0f;
                break;
            case TState.CHASE:
                NavMeshAgent.isStopped = false;
                break;
            case TState.ATTACK:
                break;
            case TState.HIT:
                break;
            case TState.DIE:
                break;
        }
    }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_LifeBarImage.fillAmount = m_Life;
        foreach (var t in PatrolList)
        {
            PatrolQueue.Enqueue(t);
        }
        SetIdleState();
    }

    void Update()
    {
        switch (State)
        {
            case TState.IDLE:
                State = TState.PATROL;
                break;
            case TState.PATROL:
                UpdatePatrolState();
                break;
            case TState.ALERT:
                UpdateAlertState();
                break;
            case TState.CHASE:
                UpdateChaseState();
                break;
            case TState.ATTACK:
                break;
            case TState.HIT:
                break;
            case TState.DIE:
                break;
        }
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_EyesPosition = transform.position + Vector3.up * EyesHeight;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * EyesPlayerHeight;
        Debug.DrawLine(l_EyesPosition, l_PlayerEyesPosition, SeesPlayer() ? Color.red : Color.blue);
    }

    private void LateUpdate()
    {
        UpdateLifeBarPosition();
    }
    void SetIdleState()
    {
        State = TState.IDLE;
    }

    void UpdatePatrolState()
    {
        if (HearsPlayer())
        {
            State = TState.ALERT;
            return;
        }
        if (PatrolTargetPositionArrived())
        {
            SetNextPatrolPosition();
        }
    }
    bool HearsPlayer()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= HearRangeDistance;
    }

    bool SeesPlayer()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_DirectionToPlayerXZ = l_PlayerPosition - transform.position;
        l_DirectionToPlayerXZ.y = 0.0f;
        l_DirectionToPlayerXZ.Normalize();
        Vector3 l_ForwardXZ = transform.forward;
        l_ForwardXZ.y = 0.0f;
        l_ForwardXZ.Normalize();
        Vector3 l_EyesPosition = transform.position + Vector3.up * EyesHeight;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * EyesPlayerHeight;
        Vector3 l_Direction = l_PlayerEyesPosition - l_EyesPosition;
        float l_Lenght = l_Direction.magnitude;
        l_Direction /= l_Lenght;


        Ray l_Ray = new Ray(l_EyesPosition, l_Direction);

        return Vector3.Distance(l_PlayerPosition, transform.position) < SightDistance && Vector3.Dot(l_ForwardXZ, l_DirectionToPlayerXZ) > Mathf.Cos(VisualConeAngle * Mathf.Deg2Rad / 2.0f) &&
            !Physics.Raycast(l_Ray, l_Lenght, SightLayerMask.value);
    }

    bool PatrolTargetPositionArrived()
    {
        return !NavMeshAgent.hasPath && !NavMeshAgent.pathPending && NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    void SetNextPatrolPosition()
    {
        PatrolQueue.Enqueue(PatrolQueue.Peek());
        PatrolQueue.Dequeue();
        NavMeshAgent.destination = PatrolQueue.Peek().position;
    }

    void UpdateAlertState()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self);
        if (RotateTimer < 0)
        {
            State = TState.PATROL;
        }
        RotateTimer -= Time.deltaTime;
        if (SeesPlayer())
        {
            State = TState.CHASE;
        }
    }

    void UpdateChaseState()
    {
        Vector3 PlayerPos = GameController.GetGameController().GetPlayer().transform.position;
        NavMeshAgent.SetDestination(PlayerPos);
        if(Vector3.Distance(PlayerPos, this.transform.position) < 3.0f)
        {
            NavMeshAgent.isStopped = true;
            State = TState.ATTACK;
        }
        if (Vector3.Distance(PlayerPos, this.transform.position) > 6.0f)
        {
            State = TState.PATROL;
        }
    }

    public void Hit(float Life)
    {
        m_Life -= Life;
        m_LifeBarImage.fillAmount = m_Life;
        Debug.Log("Hit there" + Life);
    }

    void Die()
    {
        if(m_Life <= 0)
        {

        }

    }

    void UpdateLifeBarPosition()
    {
        Vector3 l_position = GameController.GetGameController().GetPlayer().m_Camera.WorldToViewportPoint(m_LifeBarAnchorPosition.position);
        m_LifeBarRectTransform.anchoredPosition = new Vector3(l_position.x * 1920.0f, -(1080.0f - l_position.y * 1080.0f), 0.0f);
        m_LifeBarRectTransform.gameObject.SetActive(l_position.z > 0.0f);
    }


}
