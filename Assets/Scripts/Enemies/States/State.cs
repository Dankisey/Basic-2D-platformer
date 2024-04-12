using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private float _targetReachedMaxDistance;
    [SerializeField] private float _maxSpeed;

    protected Animator Animator;
    protected Transform Transform;
    protected Transform Target;
    protected Rigidbody2D Rigidbody;
    protected float MaxSpeed => _maxSpeed;

    public bool NeedTransit { get; protected set; }

    public void SetUp(Animator animator, Transform self, Rigidbody2D rigidbody)
    {
        Animator = animator;
        Transform = self;
        Rigidbody = rigidbody;
        OnSetUp();
    }

    protected virtual void OnSetUp()
    {

    }

    public virtual void Enter(Transform followingTarget)
    {
        Target = followingTarget;
    }

    public virtual void UpdateState(float deltaTime) // Not the best name, but unity is not happy with just "Update"
    {

    }

    public virtual void Exit()
    {

    }

    public virtual State GetNext() => this;

    protected void LookToTarget()
    {
        bool targetIsRight = Target.position.x - Transform.position.x > 0;
        float scaleX = targetIsRight ? 1 : -1;

        Transform.localScale = new Vector2(scaleX, Transform.localScale.y);
    }

    protected bool TargetReached()
    {
        return Mathf.Abs(Target.position.x - Transform.position.x) <= _targetReachedMaxDistance;
    }
}