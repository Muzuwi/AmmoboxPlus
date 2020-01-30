using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Graphics.Shaders;

namespace AmmoboxPlus {
    class AmmoboxHelpfulMethods {
        //  Projectile follows enemy, Chlorophyte bullet-like behavior, vanilla
        public static void chaseEnemy(int projid, int projType) {
            //  Find projectile
            bool found = false;
            Projectile projectile = new Projectile();
            int projectileIndex = 0;
            foreach (Projectile p in Main.projectile) {
                if (p.type == projType && projid == p.identity) {
                    projectile = p;
                    found = true;
                    break;
                }
                projectileIndex++;
            }
            if (!found) return;


            float velVector = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float locAIzero = projectile.localAI[0];
            if ((double)locAIzero == 0.0) {
                projectile.localAI[0] = velVector;
                locAIzero = velVector;
            }
            if (projectile.alpha > 0)
                projectile.alpha = projectile.alpha - 25;
            if (projectile.alpha < 0)
                projectile.alpha = 0;

            float projX = (float)projectile.position.X;
            float projY = (float)projectile.position.Y;
            float maxDistance = 300f;
            bool foundValidTarget = false;
            int detectedTargetID = 0;
            if ((double)projectile.ai[1] == 0.0) {
                for (int index = 0; index < 200; ++index) {
                    if (Main.npc[index].CanBeChasedBy((object)projectile, false) && ((double)projectile.ai[1] == 0.0 || (double)projectile.ai[1] == (double)(index + 1))) {
                        float npcCenterX = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        float npcCenterY = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                        float npcProjDist = Math.Abs((float)projectile.position.X + (float)(projectile.width / 2) - npcCenterX) + Math.Abs((float)projectile.position.Y + (float)(projectile.height / 2) - npcCenterY);
                        if ((double)npcProjDist < (double)maxDistance && Collision.CanHit(new Vector2((float)projectile.position.X + (float)(projectile.width / 2), (float)projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height)) {
                            maxDistance = npcProjDist;
                            projX = npcCenterX;
                            projY = npcCenterY;
                            foundValidTarget = true;
                            detectedTargetID = index;
                        }
                    }
                }
                if (foundValidTarget)
                    projectile.ai[1] = (float)(detectedTargetID + 1);
                foundValidTarget = false;
            }


            if ((double)projectile.ai[1] > 0.0) {
                int index = (int)((double)projectile.ai[1] - 1.0);
                if (Main.npc[index].active && Main.npc[index].CanBeChasedBy((object)projectile, true) && !Main.npc[index].dontTakeDamage) {
                    if ((double)Math.Abs((float)projectile.position.X + (float)(projectile.width / 2) - ((float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2))) + (double)Math.Abs((float)projectile.position.Y + (float)(projectile.height / 2) - ((float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2))) < 1000.0) {
                        foundValidTarget = true;
                        projX = (float)Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        projY = (float)Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                    }
                }
                else
                    projectile.ai[1] = 0.0f;
            }
            if (!projectile.friendly)
                foundValidTarget = false;
            if (foundValidTarget) {
                double projVelOrSomethingElse = (double)locAIzero;
                Vector2 projCenter = new Vector2((float)(projectile.position.X + (double)projectile.width * 0.5), (float)(projectile.position.Y + (double)projectile.height * 0.5));
                float distCenterX = projX - (float)projCenter.X;
                float distCenterY = projY - (float)projCenter.Y;
                double distCenter = Math.Sqrt((double)distCenterX * (double)distCenterX + (double)distCenterY * (double)distCenterY);
                float num11 = (float)(projVelOrSomethingElse / distCenter);
                float num12 = distCenterX * num11;
                float num13 = distCenterY * num11;
                int num14 = 8;
                projectile.velocity.X = (float)((projectile.velocity.X * (double)(num14 - 1) + (double)num12) / (double)num14);
                projectile.velocity.Y = (float)((projectile.velocity.Y * (double)(num14 - 1) + (double)num13) / (double)num14);
            }
            Main.projectile[projectileIndex] = projectile;
        }

        //  Spawns effects for rocket explosion, vanilla style
        public static void explodeRocket(int shot, int projid, int projtype, Color color = default(Color), bool largeBlast=false, bool skipDamage=false) {
            //  Run only on the owner client 
            //if (Main.myPlayer != owner) return;
            bool found = false;
            Projectile projectile = new Projectile();
            foreach(Projectile p in Main.projectile) {
                if(p.type == projtype && projid == p.identity) {
                    projectile = p;
                    found = true;
                    break;
                }
            }
            if (!found) return;

            projectile.position = projectile.position + new Vector2(projectile.width / 2, projectile.height / 2);
            if (largeBlast) {
                projectile.width = 80;
                projectile.height = 80;
            } else {
                projectile.width = 22;
                projectile.height = 22;
            }
            int runSmoke, runFire;
            if (largeBlast) {
                runSmoke = 70;
                runFire = 40;
            } else {
                runSmoke = 30;
                runFire = 20;
            }
            projectile.position = projectile.position - new Vector2(projectile.width / 2, projectile.height / 2);
            for (int i = 0; i < runSmoke; i++) {
                int id = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, Scale: 1.5f, newColor: color);
                Main.dust[id].velocity *= 1.4f;
            }
            for (int i = 0; i < runFire; i++) {
                int id = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Scale: 1.5f);
                Main.dust[id].noGravity = true;
                Main.dust[id].velocity *= 7f;
                id = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Scale: 1.5f);
                Main.dust[id].velocity *= 3f;
            }

            if (shot == ItemID.RocketLauncher || shot == ItemID.ProximityMineLauncher || shot == ItemID.SnowmanCannon || shot == ItemID.FireworksLauncher || shot == ItemID.ProximityMineLauncher) {
                Main.PlaySound(SoundID.Item14, projectile.position);
            } else {
                Main.PlaySound(SoundID.Item62, projectile.position);
            }

            if (skipDamage) return;
            projectile.position = projectile.position + new Vector2(projectile.width / 2, projectile.height / 2);
            if (largeBlast) {
                projectile.width = 200;
                projectile.height = 200;
            } else {
                projectile.width = 128;
                projectile.height = 128;
            }
            projectile.position = projectile.position - new Vector2(projectile.width / 2, projectile.height / 2);
            projectile.Damage();
        }

        //  Create a burst of projectiles
        public static void createBurst(int type, Vector2 initialPos, int owner, int damage, float radius=10, float velocityMultiplier=1.0f, int oneInX=4, bool makeFriendly=false, int Count=16) {
            if (Main.myPlayer != owner) return;
    
            if (Main.rand.Next(oneInX) != 0) return;

            int done = 0;
            double i = 0f;
            double increment = 2.0d / Count;

            for (done = 0; done < Count; ++done) {
                if ((i >= 0 && i <= 0.5) || (i >= 1 && i <= 1.5)) {
                    double x = radius * Math.Cos(i * Math.PI);
                    double y = radius * Math.Sin(i * Math.PI);
                    Vector2 vel = new Vector2((float)x * velocityMultiplier, (float)y * velocityMultiplier);
                    int num = Projectile.NewProjectile(initialPos + new Vector2((float)x, (float)y), vel, type, damage, 0, owner);

                    if (makeFriendly) {
                        Main.projectile[num].friendly = true;
                        Main.projectile[num].hostile = false;
                    }

                    if (Main.netMode == 1) {
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num);
                    }
                } else if ((i >= 0.5 && i <= 1) || (i >= 1.5 && i < 2)) {
                    double y = radius * Math.Cos(i * Math.PI);
                    double x = radius * Math.Sin(i * Math.PI);
                    Vector2 vel = new Vector2((float)x * velocityMultiplier, (float)y * velocityMultiplier);
                    int num = Projectile.NewProjectile(initialPos + new Vector2((float)x, (float)y), vel, type, damage, 0, owner);

                    if (makeFriendly) {
                        Main.projectile[num].friendly = true;
                        Main.projectile[num].hostile = false;
                    }

                    if (Main.netMode == 1) {
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num);
                    }
                }
                i += increment;
            }
        }

        //   Miner ammo helper function
        internal static bool processMinerOreDrop(NPC target, int oneInX=100) {
            if (WorldGen.genRand.Next(oneInX) != 0) return false;
            if (target.type == NPCID.TargetDummy) return false;
            if (target.SpawnedFromStatue) return false;

            //  Load drop table
            var dropTable = new WeightedRandom<int>();
            if (!Main.hardMode) {
                foreach (int a in AmmoboxPlus.AmmoboxOreVanillaPHM) {
                    dropTable.Add(a);
                }
            }
            else if (Main.hardMode) {
                //  Include both phm and hm ores
                foreach (int a in AmmoboxPlus.AmmoboxOreVanillaPHM) {
                    dropTable.Add(a);
                }
                if (AmmoboxPlus.AmmoboxOreVanillaHM.Count > 0) {
                    foreach (int a in AmmoboxPlus.AmmoboxOreVanillaHM) {
                        dropTable.Add(a);
                    }
                }
            }

            //  Add mod-supplied ores to the drop table (if any exist)
            if (AmmoboxPlus.AmmoboxOreModded.Count > 0) {
                foreach (int a in AmmoboxPlus.AmmoboxOreModded) {
                    dropTable.Add(a);
                }
            }

            //  Spawn the item
            int index = Item.NewItem(target.position, dropTable, WorldGen.genRand.Next(10, 30));
            if (Main.netMode == 1) {
                NetMessage.SendData(21, -1, -1, Terraria.Localization.NetworkText.Empty, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            return true;
        }

        //  Blast an area
        public static void blastArea(Vector2 position, int maxRange=3, bool extraDrops=false) {
            Dictionary<Vector2, int> posTypeExtraOreDictionary = new Dictionary<Vector2, int>();

            Dictionary<int, int> allowedTypesAndItemTypes = new Dictionary<int, int>() {
                {TileID.Copper,ItemID.CopperOre },
                {TileID.Lead,ItemID.LeadOre },
                {TileID.Silver,ItemID.SilverOre },
                {TileID.Tungsten,ItemID.TungstenOre },
                {TileID.Iron,ItemID.IronOre },
                {TileID.Gold,ItemID.GoldOre },
                {TileID.Tin,ItemID.TinOre },
                {TileID.Platinum, ItemID.PlatinumOre },
                {TileID.Meteorite,ItemID.Meteorite },
                {TileID.Demonite,ItemID.DemoniteOre },
                {TileID.Crimtane,ItemID.CrimtaneOre },
                {TileID.FossilOre,ItemID.FossilOre },
                {TileID.Hellstone,ItemID.Hellstone },
                {TileID.Obsidian,ItemID.Obsidian },
                {TileID.Adamantite,ItemID.AdamantiteOre },
                {TileID.Cobalt,ItemID.CobaltOre },
                {TileID.Mythril,ItemID.MythrilOre },
                {TileID.Orichalcum,ItemID.OrichalcumOre },
                {TileID.Palladium,ItemID.PalladiumOre },
                {TileID.Titanium,ItemID.TitaniumOre },
                {TileID.Chlorophyte, ItemID.ChlorophyteOre},
            };


            int leftCornerX = (int)(position.X / 16f - maxRange), 
                leftCornerY = (int)(position.Y / 16f - maxRange);
            int rightCornerX = (int)(position.X / 16f + maxRange),
                rightCornerY = (int)(position.Y / 16f + maxRange);

            bool breakWalls = false;

            for (int i = leftCornerX; i <= rightCornerX; i++) {
                for (int j = leftCornerY; j <= rightCornerY; j++) {
                    double dist = Vector2.Distance(position / 16f, new Vector2(i, j));
                    if (dist < (double)maxRange && Main.tile[i, j] != null && Main.tile[i, j].wall == 0) {
                        breakWalls = true;
                        break;
                    }
                }
            }

            for (int i = leftCornerX; i < rightCornerX; i++) {
                for(int j = leftCornerY; j < rightCornerY; j++) {
                    double distance = Vector2.Distance(position / 16f, new Vector2(i, j));
                    if(distance < maxRange) {
                        //  Check if tile is breakable
                        bool toBreak = true;
                        if (Main.tile[i, j] != null && Main.tile[i, j].active()) {
                            toBreak = true;
                            if (Main.tileDungeon[(int)Main.tile[i, j].type] || 
                                Main.tile[i, j].type == 88 || 
                                TileID.Sets.BasicChest[(int)Main.tile[i, j].type] || 
                                Main.tile[i, j].type == 26 || 
                                Main.tile[i, j].type == 107 || 
                                Main.tile[i, j].type == 108 || 
                                Main.tile[i, j].type == 111 || 
                                Main.tile[i, j].type == 226 || 
                                Main.tile[i, j].type == 237 || 
                                Main.tile[i, j].type == 221 || 
                                Main.tile[i, j].type == 222 || 
                                Main.tile[i, j].type == 223 || 
                                Main.tile[i, j].type == 211 || 
                                Main.tile[i, j].type == 404) {
                                toBreak = false;
                            }
                            if (!Main.hardMode && Main.tile[i, j].type == 58) {
                                toBreak = false;
                            }
                            if (toBreak) {
                                int type = Main.tile[i, j].type;
                                WorldGen.KillTile(i, j, false, false, false);

                                if (WorldGen.genRand.Next(3) == 0) {
                                    if (allowedTypesAndItemTypes.ContainsKey(type)) {
                                        //Main.NewText("tile type " + type);
                                        posTypeExtraOreDictionary[new Vector2(i, j)] = allowedTypesAndItemTypes[type];
                                    }
                                }

                                if (!Main.tile[i, j].active() && Main.netMode != 0) {
                                    NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                                }
                            }
                        }
                        if (toBreak) {
                            for (int x = i - 1; x <= i + 1; x++) {
                                for (int y = j - 1; y <= j + 1; y++) {
                                    if (Main.tile[x, y] != null && Main.tile[x, y].wall > 0 && breakWalls) {
                                        WorldGen.KillWall(x, y, false);
                                        if (Main.tile[x, y].wall == 0 && Main.netMode != 0) {
                                            NetMessage.SendData(17, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if(posTypeExtraOreDictionary.Count > 0 && extraDrops) {
                foreach(var entry in posTypeExtraOreDictionary) {
                    //Main.NewText(entry.Key + " item no " + entry.Value);
                    int id = Item.NewItem(entry.Key*16f, entry.Value, 1);
                    if (Main.netMode != 0) {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id);
                    }
                }
            }
        }

        public static void createDustCircle(Vector2 position, int dustType, float radius = 10f, bool noGravity=false, bool newDustPerfect=false, int Count=4, Color color=default(Color), int width=8, int height=8, Vector2 velocity=default(Vector2), double angleOffset=0d, int shader=0) {
            int done = 0;
            double i = angleOffset;
            double increment = 2.0d / Count;

            for (done = 0; done < Count; ++done) {
                if ((i >= 0 + angleOffset && i <= 0.5 + angleOffset) || (i >= 1 + angleOffset && i <= 1.5 + angleOffset)) {
                    double x = radius * Math.Cos(i * Math.PI);
                    double y = radius * Math.Sin(i * Math.PI);
                    if (newDustPerfect) {
                        Dust dust = Dust.NewDustPerfect(position + new Vector2((float)x, (float)y), dustType, velocity, newColor: color);
                        if (noGravity) Main.dust[dust.dustIndex].noGravity = true;
                        if (shader != 0) dust.shader = GameShaders.Armor.GetSecondaryShader(shader, Main.LocalPlayer);
                    } else {
                        int id = Dust.NewDust(position + new Vector2((float)x, (float)y), width, height, dustType, velocity.X, velocity.Y, newColor: color);
                        if (noGravity) {
                            Main.dust[id].noGravity = true;
                        }
                        if (shader != 0) {
                            Main.dust[id].shader = GameShaders.Armor.GetSecondaryShader(shader, Main.LocalPlayer);
                        }
                    }
                } else if ((i >= 0.5 + angleOffset && i <= 1 + angleOffset) || (i >= 1.5 + angleOffset && i < 2 + angleOffset)) {
                    double y = radius * Math.Cos(i * Math.PI);
                    double x = radius * Math.Sin(i * Math.PI);
                    if (newDustPerfect) {
                        Dust dust = Dust.NewDustPerfect(position + new Vector2((float)x, (float)y), dustType, velocity, newColor: color);
                        if (noGravity) Main.dust[dust.dustIndex].noGravity = true;
                        if (shader != 0) dust.shader = GameShaders.Armor.GetSecondaryShader(shader, Main.LocalPlayer);

                    } else {
                        int id = Dust.NewDust(position + new Vector2((float)x, (float)y), width, height, dustType, velocity.X, velocity.Y, newColor: color);
                        if (noGravity) {
                            Main.dust[id].noGravity = true;
                        }
                        if (shader != 0) {
                            Main.dust[id].shader = GameShaders.Armor.GetSecondaryShader(shader, Main.LocalPlayer);
                        }
                    }
                }
                i += increment;
            }
        }

        public static Color getRarityColor(int rarity) {
            float num3 = (float)Main.mouseTextColor / 255f;
            Color baseColor = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            if (rarity == -11) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(175f * num3)), (int)((byte)(0f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == -10) {
                baseColor = new Color((int)((byte)(65f * num3)), (int)((byte)(255f * num3)), (int)((byte)(110f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == -1) {
                baseColor = new Color((int)((byte)(130f * num3)), (int)((byte)(130f * num3)), (int)((byte)(130f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 1) {
                baseColor = new Color((int)((byte)(150f * num3)), (int)((byte)(150f * num3)), (int)((byte)(255f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 2) {
                baseColor = new Color((int)((byte)(150f * num3)), (int)((byte)(255f * num3)), (int)((byte)(150f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 3) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(200f * num3)), (int)((byte)(150f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 4) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(150f * num3)), (int)((byte)(150f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 5) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(150f * num3)), (int)((byte)(255f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 6) {
                baseColor = new Color((int)((byte)(210f * num3)), (int)((byte)(160f * num3)), (int)((byte)(255f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 7) {
                baseColor = new Color((int)((byte)(150f * num3)), (int)((byte)(255f * num3)), (int)((byte)(10f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 8) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(255f * num3)), (int)((byte)(10f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 9) {
                baseColor = new Color((int)((byte)(5f * num3)), (int)((byte)(200f * num3)), (int)((byte)(255f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == 10) {
                baseColor = new Color((int)((byte)(255f * num3)), (int)((byte)(40f * num3)), (int)((byte)(100f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity >= 11) {
                baseColor = new Color((int)((byte)(180f * num3)), (int)((byte)(40f * num3)), (int)((byte)(255f * num3)), (int)Main.mouseTextColor);
            }
            if (rarity == -12) {
                baseColor = new Color((int)((byte)((float)Main.DiscoR * num3)), (int)((byte)((float)Main.DiscoG * num3)), (int)((byte)((float)Main.DiscoB * num3)), (int)Main.mouseTextColor);
            }
            return baseColor;
        }
    }
}

