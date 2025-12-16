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

        var copsAfterDisable = GameManager.Instance.isAfraidCops;
        if (!copsAfterDisable) return false;

        return copsAfterDisable;
    }
}
