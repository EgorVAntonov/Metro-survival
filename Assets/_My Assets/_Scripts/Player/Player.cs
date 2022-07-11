using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CustomButton movementButton_left;
    [SerializeField] private CustomButton movementButton_right;

    [Header("Components")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private SquadController squad;

    [Header("Animations")]
    [SpineAnimation]
    [SerializeField] private string idle;
    [SpineAnimation]
    [SerializeField] private string walk;

    [Header("options")]
    [SerializeField] private float speed;

    private Spine.AnimationState spineAnimator;
    private int movementDirection;
    private string currentAnimation;

    private void OnEnable()
    {
        movementButton_left.OnButtonChangedState += UpdateMovementDirection;
        movementButton_right.OnButtonChangedState += UpdateMovementDirection;
    }

    private void OnDisable()
    {
        movementButton_left.OnButtonChangedState -= UpdateMovementDirection;
        movementButton_right.OnButtonChangedState -= UpdateMovementDirection;
    }

    private void Start()
    {
        spineAnimator = skeletonAnimation.AnimationState;
    }
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (movementDirection == 0) return;

        transform.Translate(Utility.GetVectorWithX(speed * movementDirection * Time.deltaTime));
    }

    private void UpdateMovementDirection(int delta)
    {
        movementDirection += delta;
        movementDirection = Mathf.Clamp(movementDirection, -1, 1);

        UpdateMovementAnimation();
        squad.UpdateRelativePosition(movementDirection);
    }

    private void UpdateMovementAnimation()
    {
        if (movementDirection == 0)
        {
            SetAnimation(idle, true);
            return;
        }

        SetAnimation(walk, true);
        skeletonAnimation.skeleton.ScaleX = movementDirection < 0 ? -1f : 1f;
    }

    private void SetAnimation(string name, bool loop)
    {
        if (currentAnimation == name) return;

        currentAnimation = name;
        spineAnimator.SetAnimation(0, name, loop);
    }

    public void TryRecruitUnit(Unit unit)
    {
        if (squad.AlreadyHas(unit)) return;

        squad.AddUnit(unit);
    }

    public float GetWalkSpeed()
    {
        return speed;
    }
    
}
