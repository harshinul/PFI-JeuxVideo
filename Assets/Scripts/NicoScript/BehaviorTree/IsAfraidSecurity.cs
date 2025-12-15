using UnityEngine;

public class IsAfraidSecurity : Conditions
{
    GameObject cops;
    public IsAfraidSecurity(GameObject cops, bool reverseCondition = false)
    {
        this.cops = cops;
    }

    public override bool Evaluate()
    {
        if (!cops) return false;

        var copsAfterDisable = cops.GetComponent<CopsComponent>();
        if (!copsAfterDisable) return false;

        return copsAfterDisable.isAfraidCops;
    }
}
