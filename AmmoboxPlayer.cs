using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;
using System.Collections.Generic;

namespace AmmoboxPlus {
    class AmmoboxPlayer : ModPlayer {

        //  Has a target been selected already?
        internal static bool apAcupunctureFirstTarget = false;
        //  Target ID
        internal static int apAcupunctureTargetID = -1;
        //  Damage increase
        internal static int apAcupunctureDmgIncrease = 0;
        //  Currently held weapon's ammo type
        internal static int apHeldItemAmmoType = -1;
        //  Can the player use the basic belt ammo switching?
        internal bool apCanUseBeltBasic = false;
        internal bool apCanUseBeltAdvanced = false;

        /*
         *     Stuff regarding ammo swap UI
         *      Because UI classes are somehow synchronized across all clients
         *      And this needs to be isolated from all clients to work properly
         */
        //  Amount of spawned circles
        internal int circleAmount = -1;
        //  Held item type
        internal int heldItemType = -1;
        //  List of ammo types matching the properties of the held weapon
        internal List<int> ammoTypes = new List<int>();
        //  Ammo count of ammo types used
        internal Dictionary<int, int> ammoCount = new Dictionary<int, int>();
        //  Which ammo type is currently highlighted?
        internal int selectedAmmoType = -1;
        //  Which ammo type is in the first ammo slot?
        internal int currentFirstAmmoType = -1;
        //  Is held item allowed to be used with the ammo switcher?
        internal bool itemAllowed = false;
        //  Skip drawing
        internal bool doNotDraw = false;

        //  Stuff regarding ammo display UI
        internal int ammoDisplayItemID = -1;
        internal string ammoDisplayItemName = "";
        internal int ammoDisplayItemRarity = -1;



        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) {
            if (npc.GetGlobalNPC<AmmoboxGlobalNPC>().apClouded) {
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

        public override void PreUpdate() {
            apCanUseBeltBasic = false;
            apCanUseBeltAdvanced = false;
        }
    }
}