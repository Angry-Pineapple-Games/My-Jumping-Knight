using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public override void GetPowerUp(Player player)
    {
        base.GetPowerUp(player);
        player.healHealth();
    }
}
