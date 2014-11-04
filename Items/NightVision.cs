using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items
{
	public class NightVision : ModItem
	{
		public override void Effects(Player player)
		{
            player.AddBuff(12, int.MaxValue, true);
            //these are temporary to test
            player.AddBuff(17, int.MaxValue, true);
            player.AddBuff(9, int.MaxValue, true);
            player.AddBuff(111, int.MaxValue, true);
		}
	}
}