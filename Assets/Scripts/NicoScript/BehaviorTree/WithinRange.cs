using Unity.VisualScripting;
using UnityEngine;

public class WithinRange : Conditions
{
    Transform self;
    GameObject target;
    float range;
    public WithinRange(Transform self, GameObject target, float range, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.range = range;
        this.reverseCondition = reverseCondition;
    }

    public override bool Evaluate()
    {
        if (!self || !target)
            return CheckForReverse(false);

        Transform t = target.transform;
        if (!t)
            return CheckForReverse(false);

        float distance = Vector3.Distance(self.position, t.position);
        bool inRange = distance <= range;
        return CheckForReverse(inRange);
    }
}
