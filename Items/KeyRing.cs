using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using BaseMod;

namespace Testmod.Items
{
	public class KeyRing : ModItem
	{
		public override bool CanUse(Player player)
		{
			int stack = player.inventory[player.selectedItem].stack;
			int count = 0;
			for (int m = 0; m < Main.projectile.Length; m++)
			{
				Projectile p = Main.projectile[m];
				if (p != null && p.active && p.owner == player.whoAmI && p.type == ProjDef.byName["Testmod:KeyRing"].type)
				{
					count++; if (count >= 10 || count >= stack) { return false; }
				}
			}
			return true;
		}
	}
}