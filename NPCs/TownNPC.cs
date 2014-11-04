using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace Testmod.NPCs
{
    public class TownNPC : ModNPC
    {
		public override void HitEffect(int hitDirection, double damage, bool isDead)
		{
			if (Main.netMode != 2)
			{
				for (int m = 0; m < (isDead ? 20 : 5); m++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, 5, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, isDead && m % 2 == 0 ? 3f : 1f);
					if (isDead && m % 2 == 0) { Main.dust[dustID].noGravity = true; }
				}
				if (isDead)
				{
					//Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Head"], 1f);
					//Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Body"], 1f);
					//Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Body"], 1f);
					//Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Tail"], 1f);
				}
			}
		}

        public override string SetName()
        {
            int rand = Main.rand.Next(7);
            string name = "";
            switch(rand)
            {
                case 0: name = "Steve"; break;
                case 1: name = "Bob"; break;
                case 2: name = "Bill"; break;
		case 3: name = "Jeb"; break;
                case 4: name = "James"; break;
                case 5: name = "Satan"; break;
                default: name = "Richard"; break;
            }
            return name;
        }

        public override string SetChat() 
        {
            int rand = Main.rand.Next(4);
            string text = "";
            switch (rand)
            {
                case 0: text = "..."; break;
                case 1: text = "Stop Thinking, you filthy creature."; break;
                case 2: text = "You wouldn't happen to have any flesh, would you?"; break;
                default: text = "Yes, of course this is my real head. What did you think it was?"; break;
            }
            string guidesName = NPC.AnyNPCs("Vanilla:Guide") ? Main.chrName[22] : null;
            if (guidesName != null && Main.rand.Next(4) == 0){ text = "You should stay away from " + guidesName + ". He killed my people."; }
            return text;
        }

        public override void SetupShop(Chest chest, ref int index) 
        {
            /*chest.item[index].SetDefaults(2104); 
            index++;
	    chest.item[index].SetDefaults(2105); 
            index++;
            chest.item[index].SetDefaults(2106); 
            index++;
	    chest.item[index].SetDefaults(2107); 
            index++;
            chest.item[index].SetDefaults(2108); 
            index++;
	    chest.item[index].SetDefaults(2109); 
            index++;
            chest.item[index].SetDefaults(2110); 
            index++;
	    chest.item[index].SetDefaults(2111); 
            index++;
            chest.item[index].SetDefaults(2112); 
            index++;
	    chest.item[index].SetDefaults(2113); 
            index++;*/
            chest.item[index].SetDefaults(2002); 
            index++;
            chest.item[index].SetDefaults(2673); 
            index++;
        }
        public override bool ResetShop(Chest chest) { return true; }

        public override void SetChatButtons(ref string[] buttons) //you can have up to 5 buttons (0-4)
		{
			buttons[0] = "Shop";
			buttons[1] = "Expansions";
		}
		public override void OnChatButtonClicked(string[] buttons, int buttonIndex, ref bool openShop)
		{
			if (buttonIndex == 0) //Shop buttons can return simply set 'openShop' to true.
			{
				openShop = true;
			}else
			if (buttonIndex == 1)
			{
				Player player = Main.player[Main.myPlayer];
				if (player.statLifeMax2 >= 400)
				{
					Main.npcChatText = "Mmmm! Yous vury healdy.";
				}else
				if (player.statLifeMax2 >= 300)
				{
					Main.npcChatText = "Yous somewut healdy.";
				}else
				if (player.statLifeMax2 >= 200)
				{
					Main.npcChatText = "Yous kindu healdy.";
				}else
				{
					Main.npcChatText = "Yous not healdy. Yous need maor heurts.";
				}	
				
			}
		}

		public override bool? CanHitFriendNPC(NPC npc) { return true; }
    }
}
