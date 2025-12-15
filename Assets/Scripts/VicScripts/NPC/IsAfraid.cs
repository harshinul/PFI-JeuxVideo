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
        if (!npc) return false;

        var npcAfterDisable = npc.GetComponent<NpcComponent>();
        if (!npcAfterDisable) return false;

        return npcAfterDisable.isAfraidNpc;
    }
}
