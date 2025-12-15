using UnityEngine;

public class IsAfraidCops : Conditions
{
    GameObject cops;
    public IsAfraidCops(GameObject cops, bool reverseCondition = false)
    {
        this.cops = cops;
    }

    public override bool Evaluate()
    {
        return  cops.GetComponent<CopsComponent>().isAfraid;
    }
}
