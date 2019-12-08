using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : Powerup
{
    public override void DoEffect(Player player)
    {
        player.obtainHourglass();
    }
}
