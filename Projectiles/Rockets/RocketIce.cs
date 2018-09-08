using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class RocketIce : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Rocket");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 16;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.timeLeft = 600;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            //  TODO: I have to rewrite this someday

            //  Check if enemy is in water
            //  Horrible way to actually do it, might not get all edge cases
            int posXlow = (int)target.position.X / 16;
            int posYlow = (int)target.position.Y / 16;
            int posXhi = ((int)target.position.X + target.width) / 16;
            int posYhi = ((int)target.position.Y + target.height) / 16;
            if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;

            //  Have we reached Stuck limit?
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuckLimit) {

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
                }
                else {
                    target.AddBuff(mod.BuffType<Buffs.Cold>(), 500);
                }
            } else { //  No stuck limit

                //  If enemy is in water, apply higher chance
                if (Main.tile[posXlow, posYlow].liquid > 0 || Main.tile[posXhi, posYhi].liquid > 0) {
                    if (WorldGen.genRand.Next(3) == 0) {
                        processAddBuffIce(ref target, mod.BuffType<Buffs.Stuck>(), 300);
                    }
                } else {
                    int temp = WorldGen.genRand.Next(6);
                    if (temp == 0 || temp == 1 || temp == 2 || temp == 3) {
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

        public override void AI() {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;

            //  Rocket launcher
            if(shotFrom == ItemID.RocketLauncher) {
                projectile.velocity = projectile.oldVelocity;
                if (Math.Abs(projectile.velocity.X) < 15f && Math.Abs(projectile.velocity.Y) < 15f) projectile.velocity *= 1.1f;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            }

            //  Grenade launcher
            if (shotFrom == ItemID.GrenadeLauncher) {
                //projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                //  If going to explode 
                if (projectile.ai[1] == 200) {
                    projectile.Kill();
                } else {
                    projectile.ai[1] += 1;
                }
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher) {
                if(projectile.ai[1] < 3) {
                    projectile.velocity *= 0.98f;
                }
                if(projectile.ai[1] >= 3 && projectile.alpha < 150) {
                    projectile.alpha += 1;
                }

            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon) {
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                AmmoboxHelpfulMethods.chaseEnemy(projectile.identity);
            }

            //  Common for all launchers
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(projectile.Center, Color.SkyBlue.ToVector3());

        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom ==  ItemID.RocketLauncher) {
                projectile.Kill();
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher) {
                if(projectile.ai[1] > 3) {
                    projectile.velocity = Vector2.Zero;
                } else {
                    projectile.ai[1] += 1;
                }
            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon) {
                projectile.Kill();
            }

            return true;
        }

        public override void Kill(int timeLeft) {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;
    
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, projectile.identity, largeBlast: true);
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
        }


    }
}