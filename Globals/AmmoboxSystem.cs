using System;
using System.Collections.Generic;
using AmmoboxPlus.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AmmoboxPlus
{
    class AmmoboxSystem : ModSystem
    {
        //  Has post-mech ammo been inserted into AmmoboxOreVanillaHM/AmmoboxVanillaHMAmmo already?
        internal static bool apInsertedPostMechAny = false;
        //  Same, but for all mechs
        internal static bool apInsertedPostMechAll = false;
        //  Same, but for Plantera
        internal static bool apInsertedPostPlantera = false;
        //  Same, but for Golem
        internal static bool apInsertedPostGolem = false;
        //  Same, but for Moonlord
        internal static bool apInsertedPostMoonlord = false;
        //  Same, but after HM start
        internal static bool apInsertedPostHMActive = false;

        //  Same, but always available PHM
        internal static bool apInsertedAlwaysAvailablePHM = false;

        public override void PostUpdateWorld()
        {
            //  Add always-accessible phm ammo and ore
            if (!apInsertedAlwaysAvailablePHM)
            {
                //ErrorLogger.Log("PHM inserted");
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("BulletRubber").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("BulletStarfall").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("BulletSand").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("BulletCactus").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("BulletSlime").Type] = 100;

                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartDrugged").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartStarfall").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartFrostburn").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartSand").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartCactus").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartSlime").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("DartAcupuncture").Type] = 100;

                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("ArrowDrugged").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("ArrowSand").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("ArrowCactus").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("ArrowSlime").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoPHM[Mod.Find<ModItem>("ArrowRubber").Type] = 100;

                apInsertedAlwaysAvailablePHM = true;
            }

            //  Add always-accessible hm ammo and ore
            if (Main.hardMode && !apInsertedPostHMActive)
            {
                //ErrorLogger.Log("HM inserted");
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletDrugged").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletIce").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletMiner").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletBunny").Type] = 100;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("DartBunny").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("DartIce").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("DartYing").Type] = 3;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("DartYang").Type] = 3;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("ArrowBunny").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("ArrowIce").Type] = 100;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketMiner").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketBunny").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketIce").Type] = 5;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketStarburst").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketIchor").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketFrostburn").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketCursed").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketHeart").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketHarpy").Type] = 10;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketSand").Type] = 10;


                apInsertedPostHMActive = true;
            }

            //  Add post any-mech ammo and ore
            if (Main.hardMode && NPC.downedMechBossAny && !apInsertedPostMechAny)
            {
                //ErrorLogger.Log("Mech-any inserted");
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.AdamantiteOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.CobaltOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.MythrilOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.OrichalcumOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.PalladiumOre);
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.TitaniumOre);

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.HighVelocityBullet] = 50;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.RocketIV] = 100;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketBlackhole").Type] = 10;

                apInsertedPostMechAny = true;
            }

            //  Add post all-mech ammo and ore
            if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !apInsertedPostMechAll)
            {
                //ErrorLogger.Log("Mech-all inserted");
                AmmoboxPlus.AmmoboxOreVanillaHM.Add(ItemID.ChlorophyteOre);

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.ChlorophyteBullet] = 70;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.ChlorophyteArrow] = 150;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("ArrowTrueChloro").Type] = 20;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketChlorophyte").Type] = 10;

                apInsertedPostMechAll = true;
            }

            //  Add post plantera ammo and ore
            if (Main.hardMode && NPC.downedPlantBoss && !apInsertedPostPlantera)
            {
                //ErrorLogger.Log("Plantera inserted");

                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.VenomBullet] = 50;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.VenomArrow] = 35;
                AmmoboxPlus.AmmoboxVanillaHMAmmo[ItemID.NanoBullet] = 50;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletSpectral").Type] = 100;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("ArrowSpectral").Type] = 150;

                apInsertedPostPlantera = true;
            }

            //  Add post-golem ammo and ore
            if (Main.hardMode && NPC.downedGolemBoss && !apInsertedPostGolem)
            {
                //ErrorLogger.Log("Golem inserted");

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("BulletMarked").Type] = 1;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("DartMarked").Type] = 1;
                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("ArrowMarked").Type] = 1;

                AmmoboxPlus.AmmoboxModAmmoHM[Mod.Find<ModItem>("RocketGolemfist").Type] = 10;

                apInsertedPostGolem = true;
            }

            //  Add post moonlord ammo and ore
            if (Main.hardMode && NPC.downedMoonlord && !apInsertedPostMoonlord)
            {
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

        public override void UpdateUI(GameTime gameTime)
        {
            ref var AmmoboxAmmoUI = ref AmmoboxPlus.AmmoboxAmmoUI;
            ref var AmmoboxAmmoIconHotkey = ref AmmoboxPlus.AmmoboxAmmoIconHotkey;
            ref var AmmoboxSwapUI = ref AmmoboxPlus.instance.AmmoboxSwapUI;
            ref var AmmoboxAmmoSwapHotkey = ref AmmoboxPlus.AmmoboxAmmoSwapHotkey;

            if (AmmoIconUI.vis && AmmoboxAmmoUI != null)
            {
                AmmoboxAmmoUI.Update(gameTime);
            }

            if (AmmoboxPlus.instance.AmmoboxSwapUI.visible && AmmoboxSwapUI != null)
            {
                AmmoboxSwapUI.Update(gameTime);
            }

            if (AmmoboxAmmoIconHotkey.JustPressed)
            {
                AmmoIconUI.vis = !AmmoIconUI.vis;
            }

            bool canUseBeltBasic = Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().apCanUseBeltBasic,
                 canUseBeltAdvanced = Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().apCanUseBeltAdvanced;

            //  Credits to direwolf420
            bool canOpen = Main.hasFocus &&
                !Main.drawingPlayerChat &&
                !Main.editSign &&
                !Main.editChest &&
                !Main.blockInput &&
                !Main.mapFullscreen &&
                !Main.HoveringOverAnNPC &&
                Main.LocalPlayer.talkNPC == -1 && // FIXME: wtf was this supposed to be: !Main.LocalPlayer.showItemIcon &&
                !(Main.LocalPlayer.frozen || Main.LocalPlayer.webbed || Main.LocalPlayer.stoned);


            if (AmmoboxAmmoSwapHotkey.JustPressed && (canUseBeltBasic || canUseBeltAdvanced) && Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().itemAllowed && canOpen)
            {
                //  Spawn ammo selector
                AmmoboxSwapUI.UpdateAmmoTypeList();
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().doNotDraw = false;
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().currentFirstAmmoType = Main.LocalPlayer.inventory[54].type;
                AmmoboxSwapUI.visible = true;
                AmmoboxSwapUI.spawnPosition = Main.MouseScreen;
                AmmoboxSwapUI.leftCorner = Main.MouseScreen - new Vector2(AmmoboxSwapUI.mainRadius, AmmoboxSwapUI.mainRadius);
            }
            else if (AmmoboxAmmoSwapHotkey.JustReleased && AmmoboxSwapUI.visible)
            {
                //  Destroy ammo selector
                //  Switch selected ammo
                if (Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType != -1)
                {
                    List<Tuple<int, int>> available = new List<Tuple<int, int>>();
                    //  Basic belt
                    for (int i = 54; i <= 57; i++)
                    {
                        if (Main.LocalPlayer.inventory[i].type == Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType)
                        {
                            //  Add pairs slotID - stackSize of chosen ammo
                            available.Add(new Tuple<int, int>(i, Main.LocalPlayer.inventory[i].stack));
                        }
                    }
                    //  Lihzahrd belt
                    if (canUseBeltAdvanced)
                    {
                        for (int j = 0; j < 54; j++)
                        {
                            if (Main.LocalPlayer.inventory[j].type == Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType)
                            {
                                available.Add(new Tuple<int, int>(j, Main.LocalPlayer.inventory[j].stack));
                            }
                        }
                    }

                    Tuple<int, int> chosen = available[0];
                    //  Prioritize larger stacks for switching
                    foreach (Tuple<int, int> tuple in available)
                    {
                        if (tuple.Item2 > chosen.Item2)
                        {
                            chosen = tuple;
                        }
                    }

                    //  Switch ammo stacks
                    //  Save first stack
                    Item temp = Main.LocalPlayer.inventory[54], chosenItem = Main.LocalPlayer.inventory[chosen.Item1];
                    Main.LocalPlayer.inventory[54] = chosenItem;
                    Main.LocalPlayer.inventory[chosen.Item1] = temp;


                }

                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().currentFirstAmmoType = -1;
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType = -1;
                AmmoboxSwapUI.visible = false;
                AmmoboxSwapUI.Update(gameTime);
                //  Set amount of circles drawn to -1
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().circleAmount = -1;
                //  Clear ammo types already in the list
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().ammoTypes.Clear();
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().ammoCount.Clear();
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int InventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (InventoryIndex != -1)
            {
                layers.Insert(InventoryIndex + 1, new LegacyGameInterfaceLayer(
                    "Ammobox+: Ammo Display",
                    delegate
                    {
                        if (AmmoIconUI.vis)
                        {
                            AmmoboxPlus.AmmoboxAmmoIconInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(InventoryIndex + 2, new LegacyGameInterfaceLayer(
                    "Ammobox+: Ammo Swapping",
                    delegate
                    {
                        if (AmmoboxPlus.instance.AmmoboxSwapUI.visible)
                        {
                            AmmoboxPlus.AmmoboxAmmoSwapInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
