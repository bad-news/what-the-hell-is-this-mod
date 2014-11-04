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
         * Gets the rotation to its target.
         * one for each 90d turn, not in order though
         *  
         * this may or may not be correct:
         * use pos 0 if the npc's texture is facing down.
         * use pos 1 if the npc's texture is facing up.
         * use pos 2 if the npc's texture is facing left(?)
         * use pos 3 if the npc's texture is facing right(?)
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
         * Charges at the player
         * codeEntity: what you want to have charge the target
         * target: the player
         * chargeSpeed: the speed at which the charging happens
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

        /*
         * Charges at a location
         * codeEntity: what will charge the position
         * position: the position to charge
         * chargeSpeed: how fast it goes
         */
        public static void chargePosition(CodableEntity codeEntity, Vector2 position, float chargeSpeed)
        {
            float distanceX = position.X - codeEntity.Center.X;  //get distance from the target
            float distanceY = position.Y - codeEntity.Center.Y;

            float totalDistance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY); //get the total distance

            codeEntity.velocity.X = distanceX * 5f / totalDistance * chargeSpeed;
            codeEntity.velocity.Y = distanceY * 5f / totalDistance * chargeSpeed;
        }

        /*
         * Checks if an npc is in a certain range of the player.
         *  target: The target of the npc
         *  npc: the npc itsself.
         *  range: the distance you want to check.
         *  insideRange: whether or not to check if the target is inside the range our outside the range.
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
         *  Slows down the npc by a given value.
         */
        public static void SlowDown(CodableEntity codeEnt, float speed = 0.98f)
        {
            codeEnt.velocity.X *= speed;
            codeEnt.velocity.Y *= speed;
        }

        /*
         * moves the npc to a location.
         * Pretty much just uses vanilla code, so may not be really efficent
         * codeEntity: the thing that'll be moving to the location
         * location: where you want it to move
         * movement: how fast it moves to the location
         * mult: tbh, not entirely sure what this does. you probably shouldn't mess with it.
         */
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
    }
}
