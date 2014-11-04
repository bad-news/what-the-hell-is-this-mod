using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.Items
{
    public class GatlingGun : ModItem
    {
        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback) {
            player.velocity -= velocity * 0.05f;
            //Projectile.NewProjectile(offsetVec2.X, offsetVec2.Y, velocity.X, velocity.Y, projType, damage, knockback, p.whoAmI);
            return true;
        }
        
    }
}