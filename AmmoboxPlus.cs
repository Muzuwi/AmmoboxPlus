using Terraria;
using Terraria.UI;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using AmmoboxPlus.UI;

namespace AmmoboxPlus
{
	class AmmoboxPlus : Mod
	{
        internal static AmmoboxPlus instance;

        //  Contains vanilla ore itemIDs to drop from miner ammo
        internal static List<int> AmmoboxOreVanillaPHM;

        //  Contains vanilla HM ores
        internal static List<int> AmmoboxOreVanillaHM;

        //  Contains ore itemIDs supplied by other mods 
        //  This is always added to the ore poll, as longs as Count > 0
        internal static List<int> AmmoboxOreModded;

        //  Contains vanilla ammo itemIDs, to drop from Ammo Boxes and amounts to drop
        internal static Dictionary<int, int> AmmoboxVanillaAmmo;

        //  Contains vanilla HM ammo
        internal static Dictionary<int, int> AmmoboxVanillaHMAmmo;

        //  Contains ammo itemIDs, to drop from Ammo Boxes and amounts to drop supplied by other mods (PHM)
        internal static Dictionary<int, int> AmmoboxModAmmoPHM;

        //  Contains ammo itemIDs, to drop from Ammo Boxes and amounts to drop supplied by other mods (PHM)
        internal static Dictionary<int, int> AmmoboxModAmmoHM;

        //  List containing phm bags that drop ammo boxes
        internal static List<int> AmmoboxBagAllowedPHM;
        //  List containing hm bags that drop ammo boxes
        internal static List<int> AmmoboxBagAllowedHM;

        //  Modded boss bags that are allowed to drop ammo boxes
        internal static List<int> AmmoboxBagModdedPHM;
        internal static List<int> AmmoboxBagModdedHM;

        internal static List<int> AmmoboxModdedBlacklist;

        //  Ammo icon interface elements
        internal static UserInterface AmmoboxAmmoIconInterface;
        internal static UserInterface AmmoboxAmmoSwapInterface;
        internal static AmmoIconUI AmmoboxAmmoUI;
        internal  AmmoSelectorUI AmmoboxSwapUI;
        internal static ModHotKey AmmoboxAmmoIconHotkey;
        internal static ModHotKey AmmoboxAmmoSwapHotkey;

        //  Matches the class name of a rocket type to a Tuple containing it's Item type and ProjectileType
        internal static Dictionary<string, Tuple<int, int>> RocketNameTypes;

        internal static AmmoboxGlobalItem AmmoboxGI;
        internal static AmmoboxGlobalProjectile AmmoboxGP;

        //  Global Items

		public AmmoboxPlus() {

        }

        public override void Load() {
            instance = this;
            //  Set defaults for the Lists/Dictionaries
            AmmoboxModdedBlacklist = new List<int>();
            AmmoboxBagModdedHM = new List<int>();
            AmmoboxBagModdedPHM = new List<int>();

            AmmoboxAmmoIconHotkey = RegisterHotKey("Display used ammo", "P");
            AmmoboxAmmoSwapHotkey = RegisterHotKey("Switch between ammo", "C");

            AmmoboxAmmoUI = new AmmoIconUI();
            AmmoboxAmmoUI.Activate();
            AmmoboxAmmoIconInterface = new UserInterface();
            AmmoboxAmmoIconInterface.SetState(AmmoboxAmmoUI);

            AmmoboxSwapUI = new AmmoSelectorUI();
            AmmoboxSwapUI.Activate();
            AmmoboxAmmoSwapInterface = new UserInterface();
            AmmoboxAmmoSwapInterface.SetState(AmmoboxSwapUI);

            AmmoboxGI = new AmmoboxGlobalItem();
            AmmoboxGP = new AmmoboxGlobalProjectile();

            AddGlobalItem("AmmoboxGlobalItem", AmmoboxGI);
            AddGlobalProjectile("AmmoboxGlobalProjectile", AmmoboxGP);

            resetVariables();
        }

        public override void Unload() {
            instance = null;
            AmmoboxAmmoUI = null;
            AmmoboxAmmoIconInterface = null;
            AmmoboxSwapUI = null;
            AmmoboxAmmoSwapInterface = null;
            AmmoboxGI = null;
            AmmoboxGP = null;
        }

        public override void PostSetupContent() {
            RocketNameTypes = new Dictionary<string, Tuple<int, int>>();
            //  Cache rocket item/projectile id's
            List<string> rocketClassNames = new List<string> {
                  "RocketStarburst",
                  "RocketHarpy",
                  "RocketSand",
                  "RocketGolemfist",
                  "RocketBunny",
                  "RocketCursed",
                  "RocketIchor",
                  "RocketFrostburn",
                  "RocketIce",
                  "RocketMiner",
                  "RocketBlackhole",
                  "RocketChlorophyte",
                  "RocketHeart",
            };

            foreach (string className in rocketClassNames) {
                Tuple<int, int> values = new Tuple<int, int>(ItemType(className), ProjectileType(className));
                RocketNameTypes.Add(className, values);
            }
        }

        //  TODO: Move everything sync-related to here
        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            AmmoboxMsgType type = (AmmoboxMsgType)reader.ReadByte();
            switch (type) {
                case AmmoboxMsgType.AmmoboxBunny:
                    bool action = reader.ReadBoolean();
                    int npcID = reader.ReadInt32();

                    Vector2 pos = Main.npc[npcID].position;
                    Main.npc[npcID].active = false;
                    int bunDex = NPC.NewNPC((int)pos.X, (int)pos.Y, NPCID.Bunny);
                    Main.PlaySound(SoundID.DoubleJump, pos);
                    break;
                case AmmoboxMsgType.AmmoboxMarked:
                case AmmoboxMsgType.AmmoboxClouded:
                case AmmoboxMsgType.AmmoboxCactus:
                case AmmoboxMsgType.AmmoboxSlime: {
                        int npc = reader.ReadInt32();
                        int bID = reader.ReadInt32();
                        int time = reader.ReadInt32();
                        Main.npc[npc].AddBuff(bID, time);
                        break;
                    }
                case AmmoboxMsgType.AmmoboxDelBuff: {
                        int npc = reader.ReadInt32();
                        int buffid = reader.ReadInt32();
                        Main.npc[npc].DelBuff(buffid);
                        break;

                    }
                case AmmoboxMsgType.AmmoboxUpdateVelocity: {
                        int npc = reader.ReadInt32();
                        Vector2 vel = reader.ReadVector2();
                        Main.npc[npc].velocity = vel;
                    }
                    break;
                case AmmoboxMsgType.AmmoboxBroadcastShotFrom: {
                        int who = reader.ReadInt32();
                        int projID = reader.ReadInt32();
                        short shotFrom = reader.ReadInt16();
                        int projType = reader.ReadInt32();
                        //  If server, broadcast the shotFrom value to all clients
                        if (Main.netMode == NetmodeID.Server) {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)AmmoboxMsgType.AmmoboxBroadcastShotFromToClients);
                            packet.Write(projID);
                            packet.Write(shotFrom);
                            packet.Write(projType);
                            packet.Send(-1, who);
                        }
                    break;
                }
                case AmmoboxMsgType.AmmoboxBroadcastShotFromToClients: {
                        int projID = reader.ReadInt32();
                        short shotFrom = reader.ReadInt16();
                        int projType = reader.ReadInt32();
                        //Main.NewText("Received shotFrom update from server: " + projID + ", " + shotFrom);

                        foreach(Projectile proj in Main.projectile) {
                            //  Found the projectile
                            if(proj.type == projType && proj.identity == projID) {
                                //Main.NewText("Found proj " + proj.identity + ", " + proj.whoAmI);
                                proj.GetGlobalProjectile<AmmoboxGlobalProjectile>(instance).apShotFromLauncherID = shotFrom;
                                break;
                            }
                        }
                        break;
                }


                default: break;
            }
        }

        //  Blacklisted enemies for Stuck/Cold/Slime
        public static bool isEnemyBlacklisted(int atype) {
            List<int> enemyBlacklist = new List<int>() {
                NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.ScutlixRider, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.MoonLordHand,  NPCID.SkeletronHand, NPCID.GolemHead
            };
            return (enemyBlacklist.Contains(atype) || AmmoboxModdedBlacklist.Contains(atype));
        } 

        //  Reset the Lists/Dictionaries to their default values
        public static void resetVariables() {

            //  Set up allowed bags lists
            AmmoboxBagAllowedPHM = new List<int> {
                ItemID.KingSlimeBossBag,
                ItemID.EyeOfCthulhuBossBag,
                ItemID.EaterOfWorldsBossBag,
                ItemID.BrainOfCthulhuBossBag,
                ItemID.QueenBeeBossBag,
                ItemID.WallOfFleshBossBag,
                ItemID.SkeletronBossBag
            };

            AmmoboxBagAllowedHM = new List<int> {
                ItemID.DestroyerBossBag,
                ItemID.SkeletronPrimeBossBag,
                ItemID.TwinsBossBag,
                ItemID.PlanteraBossBag,
                ItemID.GolemBossBag,
                ItemID.FishronBossBag,
                ItemID.MoonLordBossBag,
                ItemID.BossBagBetsy,
            };


            //  Load ammo and ore lists
            //  Any other items that require certain events to happen are added in ModWorld

            AmmoboxOreVanillaPHM = new List<int>() {
                ItemID.CopperOre,
                ItemID.TinOre,
                ItemID.LeadOre,
                ItemID.SilverOre,
                ItemID.TungstenOre,
                ItemID.GoldOre,
                ItemID.PlatinumOre,
                ItemID.DemoniteOre,
                ItemID.IronOre,
                ItemID.CrimtaneOre,
                ItemID.FossilOre,
                ItemID.Hellstone,
                ItemID.Obsidian
            };

            AmmoboxOreVanillaHM = new List<int>() {
            };

            AmmoboxVanillaAmmo = new Dictionary<int, int>() {
                {ItemID.SilverBullet, 100},
                {ItemID.FlamingArrow, 100},
                {ItemID.BoneArrow, 100},
                {ItemID.FrostburnArrow, 100},
                {ItemID.JestersArrow, 100},
                {ItemID.UnholyArrow, 100},
                {ItemID.HellfireArrow, 100},
                {ItemID.PoisonDart, 100},
            };

            AmmoboxVanillaHMAmmo = new Dictionary<int, int>() {
                {ItemID.CursedArrow, 150},
                {ItemID.HolyArrow, 200},
                {ItemID.IchorArrow, 150},
                {ItemID.CursedDart, 100},
                {ItemID.IchorDart, 100},
                {ItemID.CrystalDart, 100},
                {ItemID.CrystalBullet, 100},
                {ItemID.IchorBullet, 150},
                {ItemID.ExplodingBullet, 50},
                {ItemID.GoldenBullet, 50},
                {ItemID.PartyBullet, 50},
                {ItemID.CursedBullet, 150},
                {ItemID.RocketI, 100},
                {ItemID.RocketII, 100},
                {ItemID.RocketIII, 100},
            };

            AmmoboxModAmmoPHM = new Dictionary<int, int>();
            AmmoboxModAmmoHM = new Dictionary<int, int>();
            AmmoboxOreModded = new List<int>();
        }

        public override object Call(params object[] args) {
            try {
                string command = args[0] as string;

                if(command == "AddOre") {
                    int id = Convert.ToInt32(args[1]);
                    if (!AmmoboxOreModded.Contains(id)) {
                        AmmoboxOreModded.Add(id);
                    }
                    return "Success";
                } else if(command == "AddAmmo") {
                    bool hm = Convert.ToBoolean(args[1]);
                    int id = Convert.ToInt32(args[2]);
                    int amount = Convert.ToInt32(args[3]);
                    if (hm) {
                        if (!AmmoboxModAmmoHM.ContainsKey(id)) AmmoboxModAmmoHM.Add(id, amount);
                    } else {
                        if (!AmmoboxModAmmoPHM.ContainsKey(id)) AmmoboxModAmmoPHM.Add(id, amount);
                    }
                    return "Success";
                } else if (command == "AddBossBag") {
                    bool hm = Convert.ToBoolean(args[1]);
                    int id = Convert.ToInt32(args[2]);
                    if(hm) {
                        if (!AmmoboxBagModdedHM.Contains(id)) AmmoboxBagModdedHM.Add(id);
                    } else {
                        if (!AmmoboxBagModdedPHM.Contains(id)) AmmoboxBagModdedPHM.Add(id);
                    }
                    return "Success";
                } else if(command == "AddNPCToBlacklist") {
                    int id = Convert.ToInt32(args[1]);
                    if (!AmmoboxModdedBlacklist.Contains(id)) AmmoboxModdedBlacklist.Add(id);
                    return "Success";
                } else {
                    Logger.Error("[AmmoboxPlus] Invalid Call command type: " + command);
                }
            }catch(Exception exception) {
                Logger.Error("[AmmoboxPlus] Failed Call! Stack trace: " + exception.StackTrace + " What: " + exception.Message);
            }
            return "Failure";
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int InventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (InventoryIndex != -1) {
                layers.Insert(InventoryIndex + 1, new LegacyGameInterfaceLayer(
                    "Ammobox+: Ammo Display",
                    delegate {
                        if(AmmoIconUI.vis) AmmoboxAmmoIconInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(InventoryIndex + 2, new LegacyGameInterfaceLayer(
                    "Ammobox+: Ammo Swapping",
                    delegate {
                        if(AmmoboxPlus.instance.AmmoboxSwapUI.visible) AmmoboxAmmoSwapInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }

        public override void UpdateUI(GameTime gameTime) {
            if (AmmoIconUI.vis && AmmoboxAmmoUI != null) {
                AmmoboxAmmoUI.Update(gameTime);
            }

            if (AmmoboxPlus.instance.AmmoboxSwapUI.visible && AmmoboxSwapUI != null) {
                AmmoboxSwapUI.Update(gameTime);
            }

            if (AmmoboxAmmoIconHotkey.JustPressed) {
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
                Main.LocalPlayer.talkNPC == -1 &&
                !Main.LocalPlayer.showItemIcon &&
                !(Main.LocalPlayer.frozen || Main.LocalPlayer.webbed || Main.LocalPlayer.stoned);


            if (AmmoboxAmmoSwapHotkey.JustPressed && (canUseBeltBasic || canUseBeltAdvanced) && Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().itemAllowed && canOpen) {
                //  Spawn ammo selector
                AmmoboxSwapUI.UpdateAmmoTypeList();
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().doNotDraw = false;
                Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().currentFirstAmmoType = Main.LocalPlayer.inventory[54].type;
                AmmoboxSwapUI.visible = true;
                AmmoboxSwapUI.spawnPosition = Main.MouseScreen;
                AmmoboxSwapUI.leftCorner = Main.MouseScreen - new Vector2(AmmoboxSwapUI.mainRadius, AmmoboxSwapUI.mainRadius);
            } else if (AmmoboxAmmoSwapHotkey.JustReleased && AmmoboxSwapUI.visible) {
                //  Destroy ammo selector
                //  Switch selected ammo
                if (Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType != -1) {
                    List<Tuple<int, int>> available = new List<Tuple<int, int>>();
                    //  Basic belt
                    for(int i = 54; i <= 57; i++) {
                        if(Main.LocalPlayer.inventory[i].type == Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType) {
                            //  Add pairs slotID - stackSize of chosen ammo
                            available.Add(new Tuple<int, int>(i, Main.LocalPlayer.inventory[i].stack));
                        }
                    }
                    //  Lihzahrd belt
                    if (canUseBeltAdvanced) {
                        for(int j = 0; j < 54; j++) {
                            if(Main.LocalPlayer.inventory[j].type == Main.LocalPlayer.GetModPlayer<AmmoboxPlayer>().selectedAmmoType) {
                                available.Add(new Tuple<int, int>(j, Main.LocalPlayer.inventory[j].stack));
                            }
                        }
                    }

                    Tuple<int, int> chosen = available[0];
                    //  Prioritize larger stacks for switching
                    foreach(Tuple<int, int> tuple in available) {
                        if(tuple.Item2 > chosen.Item2) {
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

    }

    enum AmmoboxMsgType : byte {
        AmmoboxBunny,
        AmmoboxMarked,
        AmmoboxClouded,
        AmmoboxCactus,
        AmmoboxSlime,
        AmmoboxDelBuff,
        AmmoboxUpdateVelocity,
        AmmoboxBroadcastShotFrom,
        AmmoboxBroadcastShotFromToClients,

    }




}
