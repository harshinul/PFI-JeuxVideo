using UnityEngine;

public class IsIllegal : Conditions
{
    GameObject player;
    public IsIllegal(GameObject player, bool reverseCondition = false)
    {
        this.player = player;
    }

    public override bool Evaluate()
    {
        bool playerComponent = player.GetComponent<PlayerComponent>().isWanted;

        return playerComponent;
    }
}
