using UnityEngine;

public class IsIllegal : Conditions
{
    PlayerComponent player;
    public IsIllegal(GameObject player, bool reverseCondition = false)
    {
        this.player = player.GetComponent<PlayerComponent>();
    }

    public override bool Evaluate()
    {
        if (player == null)
        {
            Debug.LogWarning("PlayerComponent not found on the player GameObject.");
            return false;
        }
        return player.isWanted;
    }
}
