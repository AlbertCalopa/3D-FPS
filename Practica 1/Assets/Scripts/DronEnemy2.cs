using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    float SightDistance = 8.0f;
    float EyesHeight = 1.8f;
    float EyesPlayerHeight = 1.8f;
    float RotateTimer = 0.0f;

    LayerMask SightLayerMask;
    [SerializeField]
    TState _State;
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
        
        foreach(var t in PatrolList)
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
        Vector3 playerHeadPosition = GameController.GetGameController().GetPlayer().transform.position + Vector3.up * EyesPlayerHeight;
        Vector3 enemyHeadPosition = transform.position + Vector3.up * EyesHeight;
        bool isHittingPlayer = false;

        RaycastHit hit;
        Ray ray = new Ray(enemyHeadPosition, (playerHeadPosition - enemyHeadPosition).normalized * SightDistance);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Player")
            {
                isHittingPlayer = true;
            }
        }

        return isHittingPlayer;
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
    }

    void Hit(float Life)
    {
        Debug.Log("Hit there" + Life);
    }


}
