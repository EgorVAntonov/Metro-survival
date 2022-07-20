using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum ActivityName
{
    undefined,
    patrolling,
    following,
    transitioning,
    woodHarvester,
}

public class Unit : MonoBehaviour
{
    [Header("References")]
    private Player player;

    [Header("Components")]
    [SerializeField] private SkeletonAnimation defaultSkeletonAnimation;
    [SerializeField] private SkeletonAnimation woodCutterSkeletonAnimation;
    //private SkeletonAnimation currentSkeletonAnimator;
    private Spine.AnimationState defaultSpineAnimator;
    private Spine.AnimationState woodCutterSpineAnimator;
    private Spine.AnimationState currentSpineAnimator;

    [Header("Options")]
    [SerializeField] private float walkSpeed;

    [Header("Activities")]
    [SerializeField] private PatrollingActivity patrollingActivity;
    [SerializeField] private FollowingActivity followingActivity;
    [SerializeField] private TransitionActivity transitionActivity;
    [SerializeField] private HarvestingWoodActivity harvestingWoodActivity;

    private UnitActivity _activity;
    private string currentAnimationName;
    private ActivityName currentActivity = ActivityName.undefined;
    private UnitsSpawner spawner;

    private void Start()
    {
        Setup();
        SetActivity(ActivityName.patrolling);
    }

    private void Setup()
    {
        walkSpeed = 3f;
        player = FindObjectOfType<Player>();
        //currentSkeletonAnimator = defaultSkeletonAnimation;
        defaultSpineAnimator = defaultSkeletonAnimation.AnimationState;
        woodCutterSpineAnimator = woodCutterSkeletonAnimation.AnimationState;
        currentSpineAnimator = defaultSpineAnimator;
        woodCutterSkeletonAnimation.gameObject.SetActive(false);
        defaultSkeletonAnimation.gameObject.SetActive(true);

        patrollingActivity.Setup();
        followingActivity.Setup();
        transitionActivity.Setup();
        harvestingWoodActivity.Setup();
    }

    public void SetActivity(ActivityName newActivity)
    {
        if (currentActivity == newActivity) return;

        DoExitActivityAction(currentActivity);
        currentActivity = newActivity;
        DoEnterActivityAction(currentActivity);
    }

    private void DoExitActivityAction(ActivityName name)
    {
        switch (name)
        {
            case ActivityName.patrolling:
                spawner.ReduceFreeUnitCount();
                break;
            case ActivityName.following:
                break;
            case ActivityName.transitioning:
                break;
            case ActivityName.woodHarvester:
                break;
            default:
                break;
        }
    }

    private void DoEnterActivityAction(ActivityName name)
    {
        switch (name)
        {
            case ActivityName.patrolling:
                _activity = patrollingActivity;
                walkSpeed = 3f;

                break;
            case ActivityName.following:
                _activity = followingActivity;
                walkSpeed = player.GetWalkSpeed();

                break;
            case ActivityName.transitioning:
                _activity = transitionActivity;
                walkSpeed = 3f;

                break;
            case ActivityName.woodHarvester:
                _activity = harvestingWoodActivity;
                walkSpeed = 3f;

                Debug.Log("Set new animator");
                defaultSkeletonAnimation.gameObject.SetActive(false);
                woodCutterSkeletonAnimation.gameObject.SetActive(true);
                currentSpineAnimator = woodCutterSpineAnimator;
                currentAnimationName = string.Empty;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (_activity == null) return;
        
        _activity.DoActivity();
    }

    public void MoveTowards(Vector2 destination)
    {
        SetAnimation("move");
        float direction = destination.x - transform.position.x;
        SetFacingSide(direction);
        transform.position = Vector2.MoveTowards(transform.position, destination, walkSpeed * Time.deltaTime);

        void SetFacingSide(float direction)
        {
            if (direction == 0) return;
            if (direction > 0)
                transform.rotation = Quaternion.Euler(Vector3.zero);
            else
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
    }

    public void SetAnimation(string name)
    {
        if (currentAnimationName == name) return;

        currentAnimationName = name;
        currentSpineAnimator.SetAnimation(0, name, true);
    }

    public void SetAnimation(string name, bool loop)
    {
        if (currentAnimationName == name) return;

        currentAnimationName = name;
        currentSpineAnimator.SetAnimation(0, name, loop);
    }

    public void SetIndexInLayer(int index)
    {
        defaultSkeletonAnimation.gameObject.GetComponent<MeshRenderer>().sortingOrder = index;
    }

    public void SetFollowingTransform(Transform transform)
    {
        followingActivity.FollowingTransform = transform;
    }

    public void SetCurrentSawmill(Sawmill building)
    {
        transitionActivity.currentBuilding = building;
        harvestingWoodActivity.currentBuilding = building;
    }

    public bool CanBeRecruited()
    {
        return currentActivity == ActivityName.patrolling;
    }

    public void SetSpawner(UnitsSpawner spawner)
    {
        this.spawner = spawner;
        patrollingActivity.spawner = spawner;
    }
}
