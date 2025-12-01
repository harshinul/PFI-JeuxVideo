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
        float distance = Vector3.Distance(self.position, target.transform.position);
        bool inRange = distance <= range;
        return CheckForReverse(inRange);
    }
}
