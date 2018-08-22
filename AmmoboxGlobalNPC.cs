using System.Collections.Generic;
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
        public bool apStuck = false;
        //  If enemy is marked for death
        public bool apMarked = false;
        //  Has the damage multiplier been applied?
        public bool apAlreadyGrantedMulti = false;
        //  Has ore dropped from this enemy yet?
        public bool apAlreadyDroppedOre = false;
        //  Is the enemy Cold?
        public bool apCold = false;
        //  Has the enemy been frozen already?
        public bool apStuckLimit = false;

        //  Is enemy drugged (the Buff)
        public bool apDrugged = false;
        //  Is enemy going to receive damage
        public bool apDruggedGoingToReceiveDamage = false; 
        //  Drugged damage aura tick time
        public int apDruggedTick = 0;
        //  Damage to do
        public int apDruggedDamage = 0;
        //  Whether to tint the enemy
        public bool apDruggedShouldTint = false;
        //  Drugged aura cooldown
        public int apDruggedCooldown = 0;

        //  Is clouded vision applied?
        public bool apClouded = false;

        //  Is cactus shield applied?
        public bool apCactus = false;
        public bool apCactusGoingToReceiveDamage = false;
        public int apCactusTick = 0;
        public int apCactusCooldown = 0;
        public int apCactusDamage = 0;

        //  Slime?
        public bool apSlime = false;

        //  Ying/Yang ticks
        public int apYiYaTick = 0;
        public bool apYing = false;
        public bool apYang = false;
        public int apYingOwner = -1;
        public int apYangOwner = -1;


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
        
        //  Damage enemies affected by drugged 
        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            if (apDruggedGoingToReceiveDamage) {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;
                npc.lifeRegen -= apDruggedDamage/2;
                damage = apDruggedDamage/2;
                apDruggedGoingToReceiveDamage = false;
                npc.netUpdate = true;
            }
            if (apCactusGoingToReceiveDamage) {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;
                npc.lifeRegen -= apCactusDamage / 10;
                damage = apCactusDamage / 10;
                apCactusGoingToReceiveDamage = false;
                npc.netUpdate = true;
            }
        }

        //  Apply damage multiplier when Marked for Death
        public override void HitEffect(NPC npc, int hitDirection, double damage) {
            if (apMarked && !apAlreadyGrantedMulti) {
                apAlreadyGrantedMulti = true;
                npc.takenDamageMultiplier *= 1.15f;
                npc.netUpdate = true;
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

            if (apCactus) {
                for(int i = 0; i < npc.Hitbox.Height; i++) {
                    for(int j = 0; j < npc.Hitbox.Width; j++) {
                        if(i == 0 || i == npc.Hitbox.Height - 1) {
                            Dust.NewDust(npc.Hitbox.TopLeft() + new Vector2(j, i), 1, 1, 260, newColor: Color.DarkGreen);
                        } else {
                            Dust.NewDust(npc.Hitbox.TopLeft() + new Vector2(j, i), 1, 1, 260, newColor: Color.DarkGreen);
                            Dust.NewDust(npc.Hitbox.TopLeft() + new Vector2(j + npc.Hitbox.Width - 1, i), 1, 1, 260, newColor: Color.DarkGreen);
                            break;
                        }
                    }
                }
            }

            if (apClouded) {
                Dust.NewDust(npc.position, 1, 1, DustID.Sandstorm);
            }

            return true;
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
            }else if (apDruggedShouldTint) {
                drawColor = Color.Purple;
            }
        }

        public override bool PreAI(NPC npc) {
            apYiYaTick = (apYiYaTick > 0) ? apYiYaTick - 1 : 0;

            //  Drugged aura calculations
            apDruggedCooldown = (apDruggedCooldown > 0 ? apDruggedCooldown - 1 : 0);
            if (apDrugged) {
                int index = 0;
                foreach (NPC n in Main.npc) {
                    //  Calculate distance between enemies
                    float a = Math.Abs(npc.Center.X - n.Center.X);
                    float b = Math.Abs(npc.Center.Y - n.Center.Y);
                    double dist = Math.Sqrt(a * a + b * b);
                    double reach = Math.Max(npc.width, npc.height) * 1.7f;

                    if (dist < reach && n.active && !n.friendly && (n.whoAmI != npc.whoAmI) && !n.GetGlobalNPC<AmmoboxGlobalNPC>().apDrugged) {
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedGoingToReceiveDamage = true;
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedDamage = apDruggedDamage;
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedShouldTint = true;
                        Main.npc[index].netUpdate = true;
                    }
                    ++index;
                }
            }

            apCactusCooldown = (apCactusCooldown > 0 ? apCactusCooldown - 1 : 0);
            //  Totally not a rehash of drugged
            if (apCactus) {
                int index = 0;
                foreach (NPC n in Main.npc) {
                    //  Calculate distance between enemies
                    bool collision = Terraria.Collision.CheckAABBvAABBCollision(npc.Hitbox.BottomLeft(), new Vector2(npc.Hitbox.Width, npc.Hitbox.Height), n.Hitbox.BottomLeft(), new Vector2(n.Hitbox.Width, n.Hitbox.Height));

                    if ( (collision) && n.active && !n.friendly && (n.whoAmI != npc.whoAmI) && !n.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCactus) {
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCactusGoingToReceiveDamage = true;
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCactusDamage = apCactusDamage;
                        Main.npc[index].netUpdate = true;
                    }
                    ++index;
                }
            }

            npc.velocity = (apStuck) ? new Vector2(0, 0) : npc.velocity;
            return (apStuck) ? false : true;
        }
    }
}