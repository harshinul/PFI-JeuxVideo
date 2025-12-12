using UnityEngine;

public class IsAfraid : Conditions
{
    GameObject npc;
    public IsAfraid(GameObject npc, bool reverseCondition = false)
    {
        this.npc = npc;
    }

    public override bool Evaluate()
    {
        return npc.GetComponent<NpcComponent>().isAfraid;
    }
}
