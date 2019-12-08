using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{
    public override void DoEffect(Player player)
    {
        player.obtainShield();
    }
}
