using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum UnitState
{
    free,
    following,
    attack,
}

public class Unit : MonoBehaviour
{
    [Header("References")]
    private Player player;

    [Header("Components")]
    [SerializeField] private UnitTouchChecker touchChecker;
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimator;

    [Header("Animations")]
    [SpineAnimation]
    [SerializeField] private string idle;
    [SpineAnimation]
    [SerializeField] private string walk;

    [Header("options")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float patrolingRadius;

    private string currentAnimation;
    public UnitState currentState = UnitState.free;

    private Vector2 currentTargetPoint;
    private float patrollingWaitingTimer;

    public Transform followingTransform { set; private get; }

    private void OnEnable()
    {
        touchChecker.OnUnitTouched += RecruitUnit;
    }

    private void OnDisable()
    {
        touchChecker.OnUnitTouched -= RecruitUnit;
    }

    private void RecruitUnit()
    {
        if (currentState != UnitState.following)
        {
            player.TryRecruitUnit(this);
            walkSpeed = player.GetWalkSpeed();
            SetState(UnitState.following);
        }
    }

    void Start()
    {
        walkSpeed = 3f;
        player = FindObjectOfType<Player>();
        spineAnimator = skeletonAnimation.AnimationState;
        SetRandomTargetPatrollingPoint();
    }

    void Update()
    {
        DoStateAction();
    }

    private void DoStateAction()
    {
        switch (currentState)
        {
            case UnitState.free:
                DoPatrolling();
                break;
            case UnitState.following:
                DoFollowing();
                break;
            case UnitState.attack:
                DoAttack();
                break;
            default:
                break;
        }
    }

    private void DoPatrolling()
    {
        if (Vector2.Distance(transform.position, currentTargetPoint) > 0.1f)
        {
            MoveTowards(currentTargetPoint);
            return;
        }
        patrollingWaitingTimer -= Time.deltaTime;
        if (patrollingWaitingTimer > 0)
        {
            SetAnimation(idle, true);
            return;
        }
        SetRandomTargetPatrollingPoint();
    }

    private void SetRandomTargetPatrollingPoint()
    {
        float delta = Random.Range(1f, 2.5f);
        if (Random.value > 0.5)
        {
            delta *= -1f;
        }
        currentTargetPoint = new Vector2(transform.position.x + delta, transform.position.y);
        patrollingWaitingTimer = Random.Range(3f, 6f);
    }

    private void DoFollowing()
    {
        if (followingTransform == null) return;

        if (Vector2.Distance(transform.position, followingTransform.position) > 0.1f)
        {
            MoveTowards(followingTransform.position);
        }
        else
        {
            SetAnimation(idle, true);
        }
    }

    private void MoveTowards(Vector2 destination)
    {
        SetAnimation(walk, true);
        float direction = destination.x - transform.position.x;
        SetFacingSide(direction);
        transform.position = Vector2.MoveTowards(transform.position, destination, walkSpeed * Time.deltaTime);
    }

    private void SetFacingSide(float direction)
    {
        if (direction == 0) return;

        skeletonAnimation.skeleton.ScaleX = Mathf.Sign(direction);
    }

    private void DoAttack()
    {

    }

    public void SetState(UnitState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
    }

    private void SetAnimation(string name, bool loop)
    {
        if (currentAnimation == name) return;

        currentAnimation = name;
        spineAnimator.SetAnimation(0, name, loop);
    }

    public void SetIndexInLayer(int index)
    {
        skeletonAnimation.gameObject.GetComponent<MeshRenderer>().sortingOrder = index;
    }
}
