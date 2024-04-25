using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private bool lookAtTarget;
    private Vector3 _offset, _currentVelocity;
    private readonly float transitionTime = 0.12f;

    void Start() => Init();

    void Init()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = transform.position - followTarget.position;
    }
    void LateUpdate()
    {
        Vector3 targetPosition = followTarget.position + _offset;
        transform.position = Vector3.SmoothDamp(current: transform.position,
                                                target: targetPosition,
                                                currentVelocity: ref _currentVelocity,
                                                smoothTime: transitionTime);

        if (lookAtTarget) transform.LookAt(followTarget);
    }
}