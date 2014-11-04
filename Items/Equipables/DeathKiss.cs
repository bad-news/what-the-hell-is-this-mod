using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items.Equipables
{
	public class DeathKiss : ModItem
	{
		public override void Effects(Player player)
		{
            player.starCloak = 3;
            player.longInvince = true;
            player.bee = true;
            player.panic = true;
		}
	}
}