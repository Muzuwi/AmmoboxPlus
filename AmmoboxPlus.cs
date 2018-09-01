using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace AmmoboxPlus
{
	class AmmoboxPlus : Mod
	{
        internal static AmmoboxPlus instance;

        //  UNUSED
        //  Contains vanilla ore itemIDs to drop from miner ammo
        public static List<int> AmmoboxOreVanillaPHM;
        
        //  UNUSED
        //  Contains vanilla HM ores
        public static List<int> AmmoboxOreVanillaHM;

        //  UNUSED
        //  Contains ore itemIDs supplied by other mods 
        //  This is always added to the ore poll, as longs as Count > 0
        public static List<int> AmmoboxOreModded;

        //  Contains vanilla ammo itemIDs, to drop from Ammo Boxes and amounts to drop
        public static Dictionary<int, int> AmmoboxVanillaAmmo;

        //  Contains vanilla HM ammo
        public static Dictionary<int, int> AmmoboxVanillaHMAmmo;

        //  Contains ammo itemIDs, to drop from Ammo Boxes and amounts to drop supplied by other mods (PHM)
        public static Dictionary<int, int> AmmoboxModAmmoPHM;

        //  Contains ammo itemIDs, to drop from Ammo Boxes and amounts to drop supplied by other mods (PHM)
        public static Dictionary<int, int> AmmoboxModAmmoHM;

        //  List containing phm bags that drop ammo boxes
        public static List<int> AmmoboxBagAllowedPHM;
        //  List containing hm bags that drop ammo boxes
        public static List<int> AmmoboxBagAllowedHM;


        public AmmoboxPlus() {

        }

        public override void Load() {
            instance = this;
            //  Set defaults for the Lists/Dictionaries
            resetVariables();
        }

        public override void Unload() {
            instance = null;
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
                case AmmoboxMsgType.AmmoboxSlime:
                    int npc = reader.ReadInt32();
                    int bID = reader.ReadInt32();
                    int time = reader.ReadInt32();
                    Main.npc[npc].AddBuff(bID, time);
                    break;
                case AmmoboxMsgType.AmmoboxDelBuff:
                    int npcid = reader.ReadInt32();
                    int buffid = reader.ReadInt32();
                    Main.npc[npcid].DelBuff(buffid);
                    break;
                case AmmoboxMsgType.AmmoboxUpdateVelocity:
                    //  C# why
                    int npcid2 = reader.ReadInt32();
                    Vector2 vel = reader.ReadVector2();
                    Main.npc[npcid2].velocity = vel;
                    break;

                default:
                    break;
            }
        }

        //  Blacklisted enemies for Stuck/Cold/Slime
        public static bool isEnemyBlacklisted(int atype) {
            List<int> enemyBlacklist = new List<int>() {
                NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.ScutlixRider, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.MoonLordHand, NPCID.QueenBee, NPCID.SkeletronHand
            };
            return enemyBlacklist.Contains(atype);
        }

        //  Unused, maybe in the future
        public static List<int> getTechnicallySameEnemyList(int atype) {
            List<List<int>> massiveClustertruck = new List<List<int>> {
                new List<int>{7,8,9},
                new List<int>{10,11,12},
                new List<int>{13,14,15},
                new List<int>{35,36},
                new List<int>{39,40,41},
                new List<int>{87,88,89,90,91,92},
                new List<int>{95,96,97},
                new List<int>{98,99,100},
                new List<int>{113,114},
                new List<int>{117,118,119},
                new List<int>{127,128,129,130,131},
                new List<int>{134,135,136},
                new List<int>{396,397,398,400,401},
                new List<int>{402,403,404},
                new List<int>{412,413,414},
                new List<int>{454,455,456,457,458,459},
                new List<int>{510,511,512},
                new List<int>{513,514,515}
            };

            foreach(List<int> list in massiveClustertruck) {
                if (list.Contains(atype)) {
                    return list;
                }
            }
            return new List<int> { atype };
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
                {ItemID.RocketIII, 100}
            };

            AmmoboxModAmmoPHM = new Dictionary<int, int>();
            AmmoboxModAmmoHM = new Dictionary<int, int>();
            AmmoboxOreModded = new List<int>();
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
    }

}
