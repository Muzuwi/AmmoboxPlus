using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.NPCs {
    public class AmmoboxGlobalNPC : GlobalNPC {
        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        //  Is enemy stuck in place?
        internal bool apStuck = false;
        //  If enemy is marked for death
        internal bool apMarked = false;
        //  Has the damage multiplier been applied?
        internal bool apAlreadyGrantedMulti = false;
        //  Has ore dropped from this enemy yet?
        internal bool apAlreadyDroppedOre = false;
        //  Is the enemy Cold?
        internal bool apCold = false;
        //  Has the enemy been frozen already?
        internal bool apStuckLimit = false;

        //  Is enemy drugged (the Buff)
        internal bool apDrugged = false;
        //  Whether to tint the enemy
        internal bool apDruggedShouldTint = false;
        //  Drugged aura cooldown
        internal int apDruggedCooldown = 0;
        internal int apDruggedTick = 0;

        //  Is clouded vision applied?
        internal bool apClouded = false;

        //  Is cactus shield applied?
        internal bool apCactus = false;
        internal int apCactusCooldown = 0;

        //  Slime?
        internal bool apSlime = false;

        //  Ying/Yang ticks
        internal int apYiYaTick = 0;
        internal bool apYing = false;
        internal bool apYang = false;

        //  Extra heart tick
        internal int apExtraHeartTick = 0;
        internal int apExtraManaTick = 0;

        //  Reset flags
        public override void ResetEffects(NPC npc) {
            apMarked = false;
            apStuck = false;
            apCold = false;
            apDrugged = false;
            apDruggedShouldTint = false;
            apClouded = false;
            apCactus = false;
            apSlime = false;
        }

        //  Defaults, should probably update this at some point
        public override void SetDefaults(NPC npc) {
            apMarked = false;
            apAlreadyGrantedMulti = false;
            apStuck = false;
            apCold = false;
            apCactus = false;
        }
        
        //  Apply damage multiplier when Marked for Death
        public override void HitEffect(NPC npc, int hitDirection, double damage) {
            if (apMarked && !apAlreadyGrantedMulti) {
                apAlreadyGrantedMulti = true;
                npc.takenDamageMultiplier += 0.15f;
                npc.netUpdate = true;
            }
        }

        public override void NPCLoot(NPC npc) {
            if(npc.boss && !Main.expertMode) {
                if (Main.hardMode) {
                    int id = Item.NewItem(npc.getRect(), mod.ItemType("AmmoBoxPlus"));
                    if(Main.netMode != 0) {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id);
                    }
                } else {
                    int id = Item.NewItem(npc.getRect(), mod.ItemType("AmmoBox"));
                    if (Main.netMode != 0) {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id);
                    }
                }
            }
            if(Main.rand.Next(10) == 0 && apExtraHeartTick > 0) {
                int id1 = Item.NewItem(npc.getRect(), ItemID.Heart);
                int id2 = Item.NewItem(npc.getRect(), ItemID.Heart);
                if(Main.netMode != 0) {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id1);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id2);
                }
            }
            if (Main.rand.Next(10) == 0 && apExtraManaTick > 0) {
                int id1 = Item.NewItem(npc.getRect(), ItemID.Star);
                int id2 = Item.NewItem(npc.getRect(), ItemID.Star);
                if (Main.netMode != 0) {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id1);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, id2);
                }
            }
        }

        //  Draw crosshair when Marked for Death and Stuck
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) {
            if (npc.realLife != -1 && (npc.realLife != npc.whoAmI)) return true;
            if (apMarked || apStuck) {
                float elSizeX = 32;
                float elSizeY = 56;
                float spacer = (npc.type == NPCID.DD2Betsy ? 60 : 36);  //  TODO: Include other large-sprite enemies
                Vector2 npcPos = npc.Top;
                Vector2 distToNpc = npcPos - Main.screenPosition;
                Vector2 finalPos;

                if (apMarked && !apStuck) {
                    finalPos = distToNpc - new Vector2(0.5f * elSizeX, elSizeY + spacer);
                    spriteBatch.Draw(mod.GetTexture("UI/Marked"), finalPos, new Color(255, 255, 255, 255));
                }
                else if (!apMarked && apStuck) {
                    finalPos = distToNpc - new Vector2(0.5f * elSizeX, elSizeY + spacer);
                    spriteBatch.Draw(mod.GetTexture("UI/Iced"), finalPos, new Color(255, 255, 255, 255));
                }
                else if (apMarked && apStuck) {
                    finalPos = distToNpc - new Vector2(0.5f * elSizeX, elSizeY + spacer);
                    spriteBatch.Draw(mod.GetTexture("UI/FrozenMark"), finalPos, new Color(255, 255, 255, 255));
                }
            }

            if (apDrugged) {
                int radius = Math.Max(npc.width, npc.height) * 2;
                for(int i = 0; i < 360; i++) {
                    if(i < 90 || (i > 180 && i < 270)) {
                        double x = radius * Math.Cos(i * Math.PI / 180);
                        double y = radius * Math.Sin(i * Math.PI / 180);
                        Dust.NewDust(new Vector2((float)x, (float)y) + npc.Center, 8, 8, 260, newColor: Color.Blue);
                    }
                    else if ((i > 90 && i < 180) || i > 270) {
                        double x = radius * Math.Sin(i * Math.PI / 180);
                        double y = radius * Math.Cos(i * Math.PI / 180);
                        Dust.NewDust(new Vector2((float)x, (float)y) + npc.Center, 8, 8, 260, newColor: Color.Blue);
                    }
                }
            }

            if (apClouded) {
                Dust.NewDustDirect(npc.Center, 2, 2, 32);
            }

            return true;
        }

        /*
         *  Fix glitches caused by a multi-part enemy being killed by ice ammo  
         */ 
        public override bool CheckDead(NPC npc) {
            if(npc.GetGlobalNPC<AmmoboxGlobalNPC>().apStuck && npc.realLife != -1) {
                int index = 0;
                foreach (NPC n in Main.npc) {
                    if(n.whoAmI == npc.realLife || n.realLife == npc.realLife) {
                        n.StrikeNPC(n.life, 0, 1, noEffect: true);
                    }
                    ++index;
                }
                return true;
            } else {
                return true;
            }
        }

        /*
         * This is a hackzone, because I couldn't get Frozen and Chilled to be applied on NPCs
         */

        //  Draw blue-ish tint over enemy affected by Stuck and Cold
        public override void DrawEffects(NPC npc, ref Color drawColor) {
            if (apStuck || (apStuck && apCold)) {
                drawColor.B = 255;
                drawColor.R = 140;
            } else if (apCold) {
                drawColor.B = 255;
                drawColor.R = 200;
            }
            if (apDruggedShouldTint) {
                drawColor = Color.Purple;
            }
            if (apClouded) {
                drawColor = Color.Yellow;
            }
        }

        public override bool PreAI(NPC npc) {
            apYiYaTick = (apYiYaTick > 0) ? apYiYaTick - 1 : 0;

            //  Drugged aura calculations
            apDruggedCooldown = (apDruggedCooldown > 0 ? apDruggedCooldown - 1 : 0);
            apDruggedTick = (apDruggedTick > 0 ? apDruggedTick - 1 : 0);
            if (apDrugged && apDruggedTick == 0) {
                int index = 0, appliedCount = 0;
                foreach (NPC n in Main.npc) {
                    //  Calculate distance between enemies
                    float a = Math.Abs(npc.Center.X - n.Center.X);
                    float b = Math.Abs(npc.Center.Y - n.Center.Y);
                    double dist = Math.Sqrt(a * a + b * b);
                    double reach = Math.Max(npc.width, npc.height) * 1.7f;

                    if (dist < reach && n.active && !n.friendly && (n.whoAmI != npc.whoAmI) && !n.GetGlobalNPC<AmmoboxGlobalNPC>().apDrugged && !n.dontTakeDamage) {
                        if(n.realLife == npc.whoAmI) {
                            n.StrikeNPC(npc.damage / 8, 1, 0);
                        } else {
                            n.StrikeNPC(npc.damage / 2, 1, 0);
                        }
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedShouldTint = true;
                        Main.npc[index].netUpdate = true;
                        ++appliedCount;
                    }
                    ++index;
                }
                if(appliedCount > 0) apDruggedTick = 120;
            }

            apCactusCooldown = (apCactusCooldown > 0 ? apCactusCooldown - 1 : 0);
            //  Totally not a rehash of drugged
            if (apCactus) {
                int index = 0;
                foreach (NPC n in Main.npc) {
                    //  Calculate distance between enemies
                    bool collision = Collision.CheckAABBvAABBCollision(npc.Hitbox.BottomLeft(), new Vector2(npc.Hitbox.Width, npc.Hitbox.Height), n.Hitbox.BottomLeft(), new Vector2(n.Hitbox.Width, n.Hitbox.Height));
                    if ( (collision) && n.active && !n.friendly && (n.whoAmI != npc.whoAmI) && !n.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus && n.GetGlobalNPC<AmmoboxGlobalNPC>().apCactusCooldown == 0 && !n.dontTakeDamage) {
                        if (n.position.X > npc.position.X) {
                            n.StrikeNPC(npc.damage / 4, 9.0f, 1);
                        }else {
                            n.StrikeNPC(npc.damage / 4, 9.0f, -1);
                        }
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>().apCactusCooldown = 300;
                        Main.npc[index].netUpdate = true;
                    }
                    ++index;
                }
            }

            npc.velocity = (apStuck) ? new Vector2(0, 0) : npc.velocity;
            if(npc.type == 222 && apStuck) {
                npc.ai[0] = 2;
                npc.ai[1] = 0;
            }
            return (apStuck) ? false : true;
        }

        public override void AI(NPC npc) {
            apExtraManaTick = (apExtraManaTick > 0 ? apExtraManaTick - 1 : 0);
            apExtraHeartTick = (apExtraHeartTick > 0 ? apExtraHeartTick - 1 : 0);
            if (apSlime || apCold) {
                float slimeVelocityBossMulti = 0.993f;
                float slimeVelocityNpcMulti = 0.97f;
                float iceVelocityBossMulti = 0.96f;
                float iceVelocityNpcMulti = 0.93f;

                if (Main.netMode == 2) {
                    if (npc.type == NPCID.QueenBee) {
                        if ((npc.ai[0] == 0 && npc.ai[1] % 2 != 0)) {
                            //  Screw queen bee
                            return;
                        }
                    }

                    if (npc.boss) {
                        if (apSlime) {
                            npc.velocity *= slimeVelocityBossMulti;
                        } else if (apCold) {
                            if (npc.type == NPCID.QueenBee) {
                                npc.velocity *= 0.98f;
                            } else {
                                npc.velocity *= iceVelocityBossMulti;
                            }
                        }
                    } else {
                        if (apSlime) {
                            npc.velocity *= slimeVelocityNpcMulti;
                        } else if (apCold) {
                            npc.velocity *= iceVelocityNpcMulti;
                        }
                    }
                    npc.netUpdate = true;

                    var packet = mod.GetPacket();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxUpdateVelocity);
                    packet.Write(npc.whoAmI);
                    packet.WriteVector2(npc.velocity);
                    packet.Send();
                    npc.netUpdate = true;
                    npc.netUpdate2 = true;
                } else if (Main.netMode == 0) {
                    if (npc.type == NPCID.QueenBee) {
                        if ((npc.ai[0] == 0 && npc.ai[1] % 2 != 0)) {
                            //  Screw queen bee
                            return;
                        }
                    }
                    if (npc.boss) {
                        if (apSlime) {
                            npc.velocity *= slimeVelocityBossMulti;
                        } else if (apCold) {
                            if (npc.type == NPCID.QueenBee) {
                                npc.velocity *= 0.98f;
                            } else {
                                npc.velocity *= iceVelocityBossMulti;
                            }
                        }
                    } else {
                        if (apSlime) {
                            npc.velocity *= slimeVelocityNpcMulti;
                        } else if (apCold) {
                            npc.velocity *= iceVelocityNpcMulti;
                        }
                    }
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            if (type == NPCID.ArmsDealer) {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.ItemAmmoBelt>());
                    ++nextSlot;
                }

                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss) {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.ItemLihzahrdBelt>());
                    ++nextSlot;
                }
            }
        }
    }
}