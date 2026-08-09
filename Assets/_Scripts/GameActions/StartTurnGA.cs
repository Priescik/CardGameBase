using UnityEngine;

public class StartTurnGA : GameAction
{
    public Player Player { get; private set; }
    public StartTurnGA(Player player)
    {
        Player = player;
    }
}
