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

        public override void AI()
        {
            if (!spawn)
            {   //whenever it spawns, does stuff
                NPC.NewNPC(npc.Center,NPCDef.byName["MysticalMagics:BossHandTwo"].type, 0); //spawns its brother
                attackType[0] = true;//sets its attack type to 0 (Flies above the player to the left)
                spawn = true;   //reset so it doesn't run again
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                /*if (Main.npc[i].type == NPCDef.byName["MysticalMagics:BossHandTwo"].type)
                    if (Main.npc[i].active)
                    {
                        npc.realLife = Main.npc[i].life;
                        npc.life = npc.realLife;
                    }*/
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
                ResetAttackTypes();
                justCharged = false;
                int i = Main.rand.Next(0, 3);
                attackType[i] = true;
                /*switch(Main.rand.Next(0,3))
                {
                    case 0: attackType[0] = true; break;
                    case 1: attackType[1] = true; break;
                    case 2: attackType[2] = true; break;
                    case 3: attackType[3] = true; break;
                }*/
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
            }

            if (justCharged)
            {
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
    }
}
