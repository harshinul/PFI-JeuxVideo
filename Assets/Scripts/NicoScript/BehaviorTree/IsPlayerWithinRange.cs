using UnityEngine;

public class IsPlayerWithinRange : Conditions
{
    Transform cop;
    Transform player;
    float range;

    public IsPlayerWithinRange(Transform cop, Transform player, float range)
    {
        this.cop = cop;
        this.player = player;
        this.range = range;
    }

    public override bool Evaluate()
    {
        if (cop == null || player == null) return false;
        return Vector3.Distance(cop.position, player.position) <= range;
    }
}