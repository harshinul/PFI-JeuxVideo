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
        Vector3 directionToTarget = (target.transform.position - self.position).normalized;

        float angleToTarget = Vector3.Angle(self.transform.forward, directionToTarget);

        if (angleToTarget > angleView)
        {
            return CheckForReverse(false);

        }

        if (Physics.Raycast(self.position, directionToTarget, out RaycastHit hit, 10000, mask))
        {
            if (hit.collider.gameObject != target)
            {
                return CheckForReverse(false);

            }
        }

        return CheckForReverse(true);
    }
}