using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DronEnemy : MonoBehaviour
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
    public TState m_State;
    NavMeshAgent m_NavMeshAgent;
    public List<Transform> m_PatrolTargets;
    int m_CurrentPatrolTargetId = 0;
    public float m_HearRangeDistance;
    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetIdleState();
    }

    void Update()
    {
        switch (m_State)
        {
            case TState.IDLE:
                UpdateIdleState();
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
                UpdateattackState();
                break;
            case TState.HIT:
                UpdateHitState();
                break;
            case TState.DIE:
                UpdateDieState();
                break;


        }
    }

    void SetIdleState()
    {
        m_State = TState.IDLE;
        
    }
    void UpdateIdleState()
    {
        SetPatrolState();
    }
    void SetPatrolState()
    {
        m_State = TState.PATROL;
        m_NavMeshAgent.destination = m_PatrolTargets[m_CurrentPatrolTargetId].position;
    }
    void UpdatePatrolState()
    {
        if (PatrolTargetPositionArrived())
        {
            MoveToNextPatrolPosition();
        }
        if (HearsPlayer())
        {
            SetAlertState();
        }
    }
    bool HearsPlayer()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= m_HearRangeDistance;
    }
    bool PatrolTargetPositionArrived()
    {
        return !m_NavMeshAgent.hasPath && !m_NavMeshAgent.pathPending && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete;
    }
    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolTargetId;
        if(m_CurrentPatrolTargetId >= m_PatrolTargets.Count)
        {
            m_CurrentPatrolTargetId = 0;
        }
        m_NavMeshAgent.destination = m_PatrolTargets[m_CurrentPatrolTargetId].position;
    }
    void SetAlertState()
    {
        m_State = TState.ALERT;
    }
    void UpdateAlertState()
    {

    }
    void SetIChaseState()
    {
        m_State = TState.CHASE;
    }
    void UpdateChaseState()
    {

    }
    void SetAttackState()
    {
        m_State = TState.ATTACK;
    }
    void UpdateattackState()
    {

    }
    void SetHitState()
    {
        m_State = TState.HIT;
    }
    void UpdateHitState()
    {

    }
    void SetDieState()
    {
        m_State = TState.DIE;
    }
    void UpdateDieState()
    {

    }
   

}
