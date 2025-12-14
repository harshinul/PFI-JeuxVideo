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
        return cops.GetComponent<CopsComponent>().isAfraid;
    }
}
