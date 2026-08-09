using UnityEngine;

public class EndTurnGA : GameAction
{
    /// <summary> Any reactions to this action should be performed in ReactionTiming.PRE,
    /// so that there is no collision with StartTurn being called as a POST Reaction to it 
    /// </summary>
    public Player Player { get; private set; }
    public EndTurnGA(Player player)
    {
        Player = player;
    }
}
