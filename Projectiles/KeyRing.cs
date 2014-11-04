using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;
using BaseMod;

namespace Testmod.Projectiles
{
	public class KeyRing : ModProjectile
	{
		public override void AI()
		{
			Player p = Main.player[projectile.owner];
			projectile.tileCollide = false;
			BaseMod.BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 2f, 120, 20f, 0.6f, true);
		}
	}
}