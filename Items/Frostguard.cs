using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items
{
	public class Frostguard : ModItem
	{
		public override void Effects(Player player)
		{
            player.buffImmune[46] = true;
			player.knockbackResist = Math.Min(player.knockbackResist, 0f);
			if (player.statLife > player.statLifeMax2 * 0.25f)
			{
				if (player.whoAmI == Main.myPlayer)
				{
					player.paladinGive = true;
				}else
				{
					if (player.miscCounter % 5 == 0)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].team == player.team && player.team != 0)
						{
							float distX = player.position.X - Main.player[myPlayer].position.X;
							float distY = player.position.Y - Main.player[myPlayer].position.Y;
							float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
							if (dist < 800f)
							{
								Main.player[myPlayer].AddBuff(43, 10, true);
							}
						}
					}
				}
			}else
			{
				player.AddBuff(62, 5, true);
			}
		}
	}
}