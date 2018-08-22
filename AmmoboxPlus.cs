using Terraria;
using Terraria.ID;
using Terraria.Net;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus
{
	class AmmoboxPlus : Mod
	{
        internal static AmmoboxPlus instance;

		public AmmoboxPlus() {

        }

        public override void Load() {
            instance = this;
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
                default:
                    break;
            }
        }

        //  Whitelisted bosses, aka ones that won't cause weird as hell bugs
        public static bool isBossAllowed(int atype) {
            List<int> bossAllowedSet = new List<int>() {
                NPCID.EyeofCthulhu, NPCID.KingSlime, NPCID.QueenBee, NPCID.BrainofCthulhu, NPCID.DukeFishron, NPCID.SkeletronHead, NPCID.SkeletronPrime, NPCID.Plantera,
                NPCID.Golem, NPCID.CultistBoss
            };
            return bossAllowedSet.Contains(atype);
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
    }

    enum AmmoboxMsgType : byte {
        AmmoboxBunny,
        AmmoboxMarked,
        AmmoboxClouded,
        AmmoboxCactus,
        AmmoboxSlime,
        AmmoboxDelBuff,
    }

}
