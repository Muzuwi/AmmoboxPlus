using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class ArrowIce : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Arrow");
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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {

            //  Check if enemy is in water
            //  Horrible way to actually do it, might not get all edge cases
            int posXlow = (int)target.position.X / 16;
            int posYlow = (int)target.position.Y / 16;
            int posXhi = ((int)target.position.X + target.width) / 16;
            int posYhi = ((int)target.position.Y + target.height) / 16;

            //  Have we reached Stuck limit?
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit) {

                if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;

                //  If we have more parts in the chain, apply to the rest
                if (target.realLife != -1) {
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc) {
                        if (n.realLife == target.realLife) {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else {
                    target.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
            else { //  No stuck limit

                if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;

                //  If enemy is in water, apply higher chance
                if (Main.tile[posXlow, posYlow].liquid > 0 || Main.tile[posXhi, posYhi].liquid > 0) {
                    if (WorldGen.genRand.Next(50) == 0) {
                        processAddBuffIce(ref target, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }
                else {
                    if (WorldGen.genRand.Next(100) == 0) {
                        processAddBuffIce(ref target, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }

                //  If multi-part enemy
                if (target.realLife != -1) {
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc) {
                        if (n.realLife == target.realLife) {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else {
                    target.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI() {
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(projectile.Top, Color.SkyBlue.ToVector3());
        }

        public void processAddBuffIce(ref NPC npc, int type, int time) {
            //  If multipart enemy
            if (npc.realLife != -1) {
                Main.npc[npc.realLife].AddBuff(type, time);
                Main.npc[npc.realLife].velocity = new Vector2(0, 0);
                Main.npc[npc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;

                int index = 0;
                foreach (NPC n in Main.npc) {
                    if (n.realLife == npc.realLife) {
                        Main.npc[index].AddBuff(type, time);
                        Main.npc[index].velocity = new Vector2(0, 0);
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;
                    }
                    ++index;
                }
            } else {
                npc.AddBuff(type, time);
                npc.velocity = new Vector2(0, 0);
                npc.GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/iceBullet").WithVolume(0.8f), projectile.position);
        }

    }
}