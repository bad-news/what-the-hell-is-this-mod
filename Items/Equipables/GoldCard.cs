using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items.Equipables
{
	public class GoldCard : ModItem
	{
		public override void Effects(Player player)
		{
            player.coins = true;
            player.discount = 20;
            
		}
	}
}