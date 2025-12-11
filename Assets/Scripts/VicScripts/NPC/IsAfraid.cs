using UnityEngine;

public class IsAfraid : Conditions
{
    GameObject player;
    public IsAfraid(GameObject player, bool reverseCondition = false)
    {
        this.player = player;
    }

    public override bool Evaluate()
    {
        bool playerComponent = player.GetComponent<PlayerComponent>().isWanted;

        return playerComponent;
    }
}
