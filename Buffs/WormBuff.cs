using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace mod.Buffs
{
    public class WormBuff : TAPI.ModBuff
    {     
        //NOTE: In all methods with an 'index' parameter, this is the buff's index in the buff array of the player or npc.

		//PLAYER HOOKS

        //public override void Save(Player player, BinBuffer bb) { } //called when the buff is saved.
        //public override void Load(Player player, BinBuffer bb) { } //called when the buff is loaded.
        public override void Start(Player player, int index){} //called when the buff begins.
        public override void End(Player player, int index){} //called when the buff ends.
        public override void ReApply(Player player, int index){} //called when the buff is applied again when the player already has the buff.
        public override void Effects(Player player, int index){} //called between the start and end of the buff. (for continuous buff effects)
		public override void NetSend(Player player, int index, BinBuffer bb) { }
		public override void NetReceive(Player player, int index, BinBuffer bb) { }

		//Called when an NPC damages the player this buff is applied to.
		//public override void DamagePlayer(Player player, NPC npc, int hitDir, ref int damage, ref bool crit, ref float critMult) { }
		//public override void DealtPlayer(Player player, NPC npc, int hitDir, int dmgDealt, bool crit) { }

		//Called when the player this buff is applied to attacks another player.
		//public override void DamagePVP(Player player, Player p, int hitDir, ref int damage, ref bool crit, ref float critMult) { }
		//public override void DealtPVP(Player player, Player p, int hitDir, int dmgDealt, bool crit) { }

		//called when the player this buff is applied to damages an NPC.
		//public override void DamageNPC(Player player, NPC npc, int hitDir, ref int damage, ref bool crit, ref float critMult) { }
		//public override void DealtNPC(Player player, NPC npc, int hitDir, int dmgDealt, bool crit) { }

		public override Color ModifyDrawColor(Player player, Color color) { return color; }


		//--------------------------------------------------------------------------------//

		//NPC HOOKS

        public override void Start(NPC npc, int index){} //called when the buff begins.
        public override void End(NPC npc, int index){} //called when the buff ends.
        public override void ReApply(NPC npc, int index) { } //Called when the buff is applied again when the npc already has the buff.
        public override void Effects(NPC npc, int index){} //called between the start and end of the buff. (for continuous buff effects)
		public override void NetSend(NPC npc, int index, BinBuffer bb) { }
		public override void NetReceive(NPC npc, int index, BinBuffer bb) { }

		//called when the NPC this buff is applied to damages a player.
		//public override void DamagePlayer(NPC npc, Player p, int hitDir, ref int damage, ref bool crit, ref float critChance) { }
		//public override void DealtPlayer(NPC npc, Player p, int hitDir, int dmgDealt, bool crit) { }

		//called when a player damages the NPC this buff is applied to.
		//public override void DamageNPC(NPC npc, Player p, int hitDir, ref int damage, ref bool crit, ref float critMult) { }
		//public override void DealtNPC(NPC npc, Player p, int hitDir, int dmgDealt, bool crit) { }

		//called when an NPC attacks the friendly NPC this buff is applied to, or when a friendly NPC is attacked by the NPC this buff is applied to.
		//public override void DamageFriendNPC(NPC npc, NPC n, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult) { }
		//public override void DealtFriendNPC(NPC npc, NPC n, int hitDir, int dmgDealt, float knockback, bool crit) { }

		public override Color ModifyDrawColor(NPC npc, Color color) { return color; }
    }
}