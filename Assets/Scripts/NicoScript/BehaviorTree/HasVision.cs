using UnityEngine;

public class HasVision : Conditions
{
    GameObject target;
    Transform self;
    float angleView;
    LayerMask mask;

    public HasVision(Transform self, GameObject target, float angleView, LayerMask LayerMask, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.angleView = angleView;
        this.reverseCondition = reverseCondition;
        this.mask = LayerMask;
    }
    public override bool Evaluate()
    {
        if (!self || !target)
            return CheckForReverse(false);

        Transform targetT = target.transform;
        if (!targetT)
            return CheckForReverse(false);

        Vector3 directionToTarget = (targetT.position - self.position).normalized;

        if (directionToTarget.sqrMagnitude < 0.0001f)
            return CheckForReverse(true);

        float angleToTarget = Vector3.Angle(self.forward, directionToTarget);
        if (angleToTarget > angleView)
            return CheckForReverse(false);

        float maxDistance = 1000f;
        if (Physics.SphereCast(self.position, 0.6f, directionToTarget, out RaycastHit hit, maxDistance))
        {
            if (hit.collider == null)
                return CheckForReverse(false);

            if (hit.collider.transform != targetT)
                return CheckForReverse(false);
        }

        return CheckForReverse(true);
    }
}