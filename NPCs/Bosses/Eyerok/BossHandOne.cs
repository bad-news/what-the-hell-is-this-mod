using System;

using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace DesertRedux.NPCs
{
    class BossHandOne : ModNPC
    {
        float maxSpeed = 4;
        bool lookAtPlayer = false;
        Vector2 restingLocation;
        bool moveToRestingLoc;
        bool justCharged;
        bool[] attackType = new bool[4];
        int[] attackTypeTimer = new int[4];
        bool flag = false;
        bool spawn = false; //has onspawn stuff been taken care of yet
        int brotherTwo;

        public override void AI()
        {
            if (!spawn)
            {   //whenever it spawns, does stuff
                brotherTwo = NPC.NewNPC(npc.Center, NPCDef.byName["DesertRedux:BossHandTwo"].type, 0); //spawns its brother
                attackType[0] = true;//sets its attack type to 0 (Flies above the player to the left)
                spawn = true;   //reset so it doesn't run again
            }

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                npc.TargetClosest(true);
            Player target = Main.player[npc.target];

            if (lookAtPlayer)
                npc.rotation = Helper.GetRotation(target, npc, 2);

            npc.ai[0]++;
            //Main.NewText("" + npc.ai[0]);

            if (npc.ai[0] > 600)
            {
                SetAttackTypes();
                npc.ai[0] = 0;
            }

            if (attackType[0])
            {
                restingLocation = new Vector2(target.position.X - 150, target.position.Y - 150);
                Helper.MoveToLocation(npc, restingLocation, 0.07f, 6f);
            }
            if (attackType[1])
            {
                attackTypeTimer[1]++;

                restingLocation = new Vector2(target.position.X - 150, target.position.Y);
                if (attackTypeTimer[1] <= 150)
                    Helper.MoveToLocation(npc, restingLocation, 0.07f, 6f);
                else if ((npc.Center - restingLocation).Length() < 50 && attackTypeTimer[1] > 150)
                {
                    Helper.chargePosition(npc, new Vector2(target.position.X + 300, target.position.Y), 6f);
                    justCharged = true;
                    attackTypeTimer[1] = 0;
                }
                else Helper.MoveToLocation(npc, restingLocation, 0.07f, 6f);
            }
            if (attackType[2])
            {
                attackTypeTimer[2]++;

                restingLocation = new Vector2(target.position.X, target.position.Y - 150);

                if (attackTypeTimer[2] <= 150)
                    Helper.MoveToLocation(npc, restingLocation, 0.07f, 6f);
                if ((npc.Center - restingLocation).Length() < 50 && attackTypeTimer[2] > 150)
                {
                    Helper.chargeTarget(npc, target, 4f);
                    justCharged = true;
                    attackTypeTimer[2] = 0;
                }
            }
            if (attackType[3])
            {
                restingLocation = new Vector2(target.position.X, target.position.Y);
                Helper.MoveToLocation(npc, restingLocation, 0.07f, 8f);

                attackTypeTimer[3]++;
                if (attackTypeTimer[3] > 15)
                {
                    float rot = Helper.GetRotation(target, npc, 1);

                    int proj = Projectile.NewProjectile(npc.Center, new Vector2((float)Math.Cos(rot) * -20f, (float)Math.Sin(rot) * -20f), "Eye Laser", 60, 1.2f, Main.myPlayer);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;

                    attackTypeTimer[3] = 0;
                }
            }

            if (Helper.IsInRange(target, npc, 1200, false))
                npc.position = restingLocation;

            if (justCharged)
            {   //handles charging
                npc.ai[2]++;
                if(npc.ai[2] > 30)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;

                    justCharged = false;

                    npc.ai[2] = 0;
                }
            }

            CheckHalfHP();
            Lighting.AddLight(npc.Center, Color.Yellow);
        }

        public bool CheckHalfHP()
        {
            bool flag = false;

            if (npc.life < npc.lifeMax / 2 && !flag)
            {   //if the npc is below half hp, do some stuff
                npc.damage *= 2;
                npc.defense *= 2;
                this.maxSpeed *= 2;
                flag = true;
                return true;
            }
            return false;
        }

        public void ResetAttackTypes()
        {   //resets stuff, obveously.
            for (int i = 0; i < 4; i++)
            {
                attackType[i] = false;
                attackTypeTimer[i] = 0;
            }
        }

        public void SetAttackTypes()
        {
            ResetAttackTypes();
            justCharged = false;
            int i = Main.rand.Next(0, 4);
            attackType[i] = true;

            npc.ai[3] = i;
            if (Main.npc[brotherTwo].ai[3] == i)
            {   //Makes sure that the attack types are not equal to each other. Yeah, not the most effecient.
                if (Main.npc[brotherTwo].ai[3] == 0 && npc.ai[3] == 0)
                    return; //if the attack types are both 0, then don't go any further.
                ResetAttackTypes();
                int j = Main.rand.Next(0, 3);

                if (j != i)
                {
                    attackType[j] = true;
                    return;
                }
                else
                    SetAttackTypes();
            }
        }

        public override void HitEffect(int hitDirection, double damage, bool isDead)
        {
            if (isDead)
                if (Main.npc[brotherTwo].active)
                {
                    npc.value = 0;
                    npc.boss = false;
                }
            for (int i = 0; i < 50; i++)
                Dust.NewDust(npc.position, npc.width, npc.height, 148, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
        }
    }
}
