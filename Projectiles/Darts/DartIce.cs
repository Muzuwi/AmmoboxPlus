using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartIce : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Dart");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.spriteDirection = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {

            //  Check if enemy is in water
            //  Horrible way to actually do it, might not get all edge cases
            int posXlow = (int)target.position.X / 16;
            int posYlow = (int)target.position.Y / 16;
            int posXhi = ((int)target.position.X + target.width) / 16;
            int posYhi = ((int)target.position.Y + target.height) / 16;

            //  Have we reached Stuck limit?
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuckLimit) {

                if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;

                //  If we have more parts in the chain, apply to the rest
                if (target.realLife != -1) {
                    Main.npc[target.realLife].AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc) {
                        if (n.realLife == target.realLife) {
                            Main.npc[index].AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                } else {
                    target.AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                }
            } else { //  No stuck limit

                if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;

                //  If enemy is in water, apply higher chance
                if (Main.tile[posXlow, posYlow].liquid > 0 || Main.tile[posXhi, posYhi].liquid > 0) {
                    if (WorldGen.genRand.Next(50) == 0) {
                        processAddBuffIce(ref target, mod.BuffType<Buffs.Stuck>(), 300);
                    }
                } else {
                    if (WorldGen.genRand.Next(100) == 0) {
                        processAddBuffIce(ref target, mod.BuffType<Buffs.Stuck>(), 300);
                    }
                }

                //  If multi-part enemy
                if (target.realLife != -1) {
                    Main.npc[target.realLife].AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc) {
                        if (n.realLife == target.realLife) {
                            Main.npc[index].AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                } else {
                    target.AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI() {
            for (int i = 0; i < 2; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(projectile.position, Color.DodgerBlue.ToVector3());
        }

        public void processAddBuffIce(ref NPC npc, int type, int time) {
            //  If multipart enemy
            if (npc.realLife != -1) {
                Main.npc[npc.realLife].AddBuff(type, time);
                Main.npc[npc.realLife].velocity = new Vector2(0, 0);
                Main.npc[npc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuckLimit = true;

                int index = 0;
                foreach (NPC n in Main.npc) {
                    if (n.realLife == npc.realLife) {
                        Main.npc[index].AddBuff(type, time);
                        Main.npc[index].velocity = new Vector2(0, 0);
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuckLimit = true;
                    }
                    ++index;
                }
            } else {
                npc.AddBuff(type, time);
                npc.velocity = new Vector2(0, 0);
                npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuckLimit = true;
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/iceBullet").WithVolume(0.8f), projectile.position);
        }

    }
}