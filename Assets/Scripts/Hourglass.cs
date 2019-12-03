using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : Powerup
{
    public override void GetPowerUp(Player player)
    {
        base.GetPowerUp(player);
        player.obtainHourglass();
    }
}
