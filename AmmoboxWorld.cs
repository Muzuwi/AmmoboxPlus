using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus {
    class AmmoboxWorld : ModWorld{
        //  Has post-mech ammo been inserted into AmmoboxOreVanillaHM/AmmoboxVanillaHMAmmo already?
        public static bool apInsertedPostMechAny = false;
        //  Same, but for all mechs
        public static bool apInsertedPostMechAll = false;
        //  Same, but for Plantera
        public static bool apInsertedPostPlantera = false;
        //  Same, but for Golem
        public static bool apInsertedPostGolem = false;
        //  Same, but for Moonlord
        public static bool apInsertedPostMoonlord = false;
        //  Same, but after HM start
        public static bool apInsertedPostHMActive = false;

        //  Same, but always available PHM
        public static bool apInsertedAlwaysAvailablePHM = false;


        public override void PostUpdate() {
            //  Add always-accessible phm ammo and ore
            if (!apInsertedAlwaysAvailablePHM) {
                //ErrorLogger.Log("PHM inserted");
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("BulletRubber")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("BulletStarfall")] = 100;
                //AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("BulletSand")] = 100;
                //AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("BulletCactus")] = 100;
                //AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("BulletSlime")] = 100;

                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartDrugged")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartStarfall")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartFrostburn")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartSand")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartCactus")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartSlime")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("DartAcupuncture")] = 100;

                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("ArrowDrugged")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("ArrowSand")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("ArrowCactus")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("ArrowSlime")] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[mod.ItemType("ArrowRubber")] = 100;

                apInsertedAlwaysAvailablePHM = true;
            }

            //  Add always-accessible hm ammo and ore
            if (Main.hardMode && !apInsertedPostHMActive) {
                //ErrorLogger.Log("HM inserted");
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletDrugged")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletIce")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletMiner")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletBunny")] = 100;

                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("DartBunny")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("DartIce")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("DartYing")] = 3;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("DartYang")] = 3;

                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("ArrowBunny")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("ArrowIce")] = 100;


                apInsertedPostHMActive = true;
            }

            //  Add post any-mech ammo and ore
            if (Main.hardMode && NPC.downedMechBossAny && !apInsertedPostMechAny) {
                //ErrorLogger.Log("Mech-any inserted");
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.AdamantiteOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.CobaltOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.MythrilOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.OrichalcumOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.PalladiumOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.TitaniumOre);

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.HighVelocityBullet] = 50;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.RocketIV] = 100;

                apInsertedPostMechAny = true;
            }

            //  Add post all-mech ammo and ore
            if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !apInsertedPostMechAll) {
                //ErrorLogger.Log("Mech-all inserted");
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.ChlorophyteOre);

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.ChlorophyteBullet] = 70;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.ChlorophyteArrow] = 150;

                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("ArrowTrueChloro")] = 20;

                apInsertedPostMechAll = true;
            }

            //  Add post plantera ammo and ore
            if (Main.hardMode && NPC.downedMechBossAny && !apInsertedPostPlantera) {
                //ErrorLogger.Log("Plantera inserted");

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.VenomBullet] = 50;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.VenomArrow] = 35;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.NanoBullet] = 50;

                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletSpectral")] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("ArrowSpectral")] = 150;

                apInsertedPostPlantera = true;
            }

            //  Add post-golem ammo and ore
            if (Main.hardMode && NPC.downedGolemBoss && !apInsertedPostGolem) {
                //ErrorLogger.Log("Golem inserted");

                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("BulletMarked")] = 1;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("DartMarked")] = 1;
                AmmoboxPlus.AmmoboxModAmmoHM[mod.ItemType("ArrowMarked")] = 1;


                apInsertedPostGolem = true;
            }

            //  Add post moonlord ammo and ore
            if (Main.hardMode && NPC.downedMechBossAny && !apInsertedPostMoonlord) {
                //ErrorLogger.Log("Moonlord inserted");

                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.LunarOre);
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.MoonlordArrow] = 333;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.MoonlordBullet] = 333;

                apInsertedPostMoonlord = true;
            }

            /*ErrorLogger.Log("Validating AmmoboxModAmmo ");
            foreach(var a in AmmoboxPlus.AmmoboxModAmmo) {
                ErrorLogger.Log(a.Key + ":" + a.Value);
            }*/
        }

    }
}
