using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace MysticalMagics
{
    class Helper
    {
        /*
         *  Gets the rotation to its target.
         *  @Param pos: 4 possible ints
         *  use 0 when charging a target
         *  using 2 when shooting a projectile at the player
         */
        public static float GetRotation(Player target, NPC npc, int pos)
        {
            if (pos == 0)
                return (target.Center - npc.Center).ToRotation() + -MathHelper.PiOver2;
            else if (pos == 1)
                return (target.Center - npc.Center).ToRotation() + MathHelper.PiOver2;
            else if (pos == 2)
                return (target.Center - npc.Center).ToRotation() + ((float)-Math.PI);
            else if (pos == 3)
                return (target.Center - npc.Center).ToRotation() + ((float)Math.PI);
            
            else return (target.Center - npc.Center).ToRotation() + -MathHelper.PiOver2;
        }

        public static double GetCosSin(float rot, bool cosSin)
        {
            if (cosSin)
                return Math.Cos(rot);
            else return Math.Sin(rot);
        }

        /*
         *  Charges the Target.
         *  @Param target: the player the npc is targeting.
         *  @Param npc: the npc.
         *  @Param speed: the speed at which to charge. 
         *  
         *  Sometimes doesn't work properly, not sure why :s
         */
        public static void ChargeTarget(Player target, NPC npc, float speed)
        {
            float rot = GetRotation(target, npc, 2);

            npc.velocity.X = (float)Math.Cos(rot) * -speed;
            npc.velocity.Y = (float)Math.Sin(rot) * -speed;
        }
        /*
         * use this one instead
         */
        public static void chargeTarget(CodableEntity codeEntity, Player target, float chargeSpeed)
        {
            codeEntity.velocity *= 0.98f;   //slow down

            float distanceX = target.position.X - codeEntity.Center.X;  //get distance from the target
            float distanceY = target.position.Y - codeEntity.Center.Y;

            float totalDistance = (float) Math.Sqrt(distanceX * distanceX + distanceY * distanceY); //get the total distance

            codeEntity.velocity.X = distanceX * 5f / totalDistance * chargeSpeed;
            codeEntity.velocity.Y = distanceY * 5f / totalDistance * chargeSpeed;
        }

        public static void chargePosition(CodableEntity codeEntity, Vector2 position, float chargeSpeed)
        {
            float distanceX = position.X - codeEntity.Center.X;  //get distance from the target
            float distanceY = position.Y - codeEntity.Center.Y;

            float totalDistance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY); //get the total distance

            codeEntity.velocity.X = distanceX * 5f / totalDistance * chargeSpeed;
            codeEntity.velocity.Y = distanceY * 5f / totalDistance * chargeSpeed;
        }

        /*  Checks if an npc is in a certain range of the player.
         *  @Param target: The target of the npc
         *  @Param npc: the npc itsself.
         *  @Param range: the distance you want to check.
         *  @Param inout: whether or not to check if the target is inside the range our outside the range.
         */
        public static bool IsInRange(Player target, CodableEntity npc, int range, bool insideRange)
        {
            if (insideRange)
            {
                if ((npc.Center - target.Center).Length() < range)
                    return true;
            }
            if (!insideRange)
            {
                if ((npc.Center - target.Center).Length() > range)
                    return true;
            }

            return false;
        }

        /*
         *  Slows down the npc.
         */
        public static void SlowDown(CodableEntity codeEnt, float speed = 0.98f)
        {
            codeEnt.velocity.X *= speed;
            codeEnt.velocity.Y *= speed;
        }

        public static void MoveToLocation(CodableEntity codeEntity, Vector2 location, float movement = 0.07f, float mult = 6f)  //copied mostly from terraria's source code, EoC
        {
            float distX = location.X - codeEntity.Center.X;
            float distY = location.Y - codeEntity.Center.Y;
            float distTotal = (float)System.Math.Sqrt((double)(distX * distX + distY * distY));

            distTotal = mult / distTotal;
            distX *= distTotal;
            distY *= distTotal;

            if (codeEntity.velocity.X < distX)
            {
                codeEntity.velocity.X += movement;
                if (codeEntity.velocity.X < 0f && distX > 0f)
                {
                    codeEntity.velocity.X += movement;
                }
            }
            else
            {
                if (codeEntity.velocity.X > distX)
                {
                    codeEntity.velocity.X -= movement;
                    if (codeEntity.velocity.X > 0f && distX < 0f)
                    {
                        codeEntity.velocity.X -= movement;
                    }
                }
            }
            if (codeEntity.velocity.Y < distY)
            {
                codeEntity.velocity.Y += movement;
                if (codeEntity.velocity.Y < 0f && distY > 0f)
                {
                    codeEntity.velocity.Y += movement;
                }
            }
            else
            {
                if (codeEntity.velocity.Y > distY)
                {
                    codeEntity.velocity.Y -= movement;
                    if (codeEntity.velocity.Y > 0f && distY < 0f)
                    {
                        codeEntity.velocity.Y -= movement;
                    }
                }
            }
        }

        public static void SlowDown(Projectile proj, float speed = 0.98f)
        {
            proj.velocity.X *= speed;
            proj.velocity.Y *= speed;
        }

        /*
         *  Kills the npc if it's active and its life is greater than 0.
         */
        public static void SetDead(NPC npc)
        {
            if (npc.active && npc.life > 0)
            {
                npc.active = false;
                npc.checkDead();
            }
        }
    }
}
