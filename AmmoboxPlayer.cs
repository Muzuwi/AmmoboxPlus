using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus {
    class AmmoboxPlayer : ModPlayer {

        //  Has a target been selected already?
        internal static bool apAcupunctureFirstTarget = false;
        //  Target ID
        internal static int apAcupunctureTargetID = -1;
        //  Damage increase
        internal static int apAcupunctureDmgIncrease = 0;

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) {
            if (npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apClouded) {
                if(WorldGen.genRand.Next(12) == 0) {
                    for(int i = 0; i < 5; i++) Dust.NewDust(player.position, 1, 1, DustID.Smoke);
                    player.shadowDodge = true;
                    player.shadowDodgeTimer = 5;
                }
            }
        }

        public override void OnEnterWorld(Player player) {
            //  Reset the lists/dictionaries and bools
            AmmoboxPlus.resetVariables();
            AmmoboxWorld.apInsertedPostMechAny = false;
            AmmoboxWorld.apInsertedPostMechAll = false;
            AmmoboxWorld.apInsertedPostPlantera = false;
            AmmoboxWorld.apInsertedPostGolem = false;
            AmmoboxWorld.apInsertedPostMoonlord = false;
            AmmoboxWorld.apInsertedPostHMActive = false;
            AmmoboxWorld.apInsertedAlwaysAvailablePHM = false;
        }

    }
}