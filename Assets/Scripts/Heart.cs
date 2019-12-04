using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public override void DoEffect(Player player)
    {
        player.healHealth();
    }
}
