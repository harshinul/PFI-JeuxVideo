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
            Debug.Log("No Vision: Angle too wide");
            return CheckForReverse(false);

        }

        if (Physics.SphereCast(self.position, 0.6f, directionToTarget, out RaycastHit hit)) // spherecast to have a wider vision
        {

            if (hit.collider.gameObject != target)
            {
                //Debug.Log("No Vision: Obstacle in the way");

                return CheckForReverse(false);

            }
        }


        //if (Physics.Raycast(self.position, directionToTarget, out RaycastHit hit, 10000))
        //{
        //    if (hit.collider.gameObject != target)
        //    {
        //        return CheckForReverse(false);

        //    }
        //}

        return CheckForReverse(true);
    }
}