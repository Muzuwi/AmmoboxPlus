using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;


namespace AmmoboxPlus {
    class AmmoboxPlayer : ModPlayer {

        //  Has a target been selected already?
        public static bool apAcupunctureFirstTarget = false;
        //  Target ID
        public static int apAcupunctureTargetID = -1;
        //  Damage increase
        public static int apAcupunctureDmgIncrease = 0;

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) {
            if (npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apClouded) {
                //  This actually doesn't negate the damage completely, but leaves 1 point of damage
                //  I'll fix this someday
                if(WorldGen.genRand.Next(100) == 0) {
                    for(int i = 0; i < 5; i++) Dust.NewDust(player.position, 1, 1, DustID.Smoke);
                    damage = 0;
                    crit = false;
                }
            }
        }

    }
}