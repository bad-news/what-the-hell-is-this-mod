using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items.Equipables
{
	public class SniperClip : ModItem
	{
		public override void Effects(Player player)
		{
            if (player.inventory[player.selectedItem].ranged == true)
            {
                player.scope = true;
            }
            player.rangedCrit += 15;
            player.rangedDamage += 0.15f;
            player.ammoCost80 = true;
            
		}
	}
}