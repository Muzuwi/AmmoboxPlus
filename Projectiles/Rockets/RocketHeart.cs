using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class RocketHeart : ModProjectile {

        public bool drawAura = false;
        public double rotation = 0d;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Regeneration Rocket");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 14;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.timeLeft = 600;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override bool? CanHitNPC(NPC target) {
            return false;
        }

        public override void AI() {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;

            if (drawAura) {
                if(rotation >= 2) {
                    rotation = 0;
                } else {
                    rotation += (1 / 256d);
                }

                // (0, 142, 255)
                //  0, 0x11, 255
                AmmoboxHelpfulMethods.createDustCircle(projectile.Center, 178, 256, true, true, 32, color: new Color(0, 142, 255), angleOffset: rotation, shader: 39);
                AmmoboxHelpfulMethods.createDustCircle(projectile.Center, 178, 256, true, true, 32, color: new Color(255, 0, 0), angleOffset: (1 / 32d), shader: 39);
                foreach (NPC n in Main.npc) {
                    float a = Math.Abs(projectile.Center.X - n.Center.X);
                    float b = Math.Abs(projectile.Center.Y - n.Center.Y);
                    double dist = Math.Sqrt(a * a + b * b);

                    if (dist < 192f && n.active && !n.friendly) {
                        if (Main.player[projectile.owner].statLife < Main.player[projectile.owner].statLifeMax) {
                            n.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apExtraHeartTick = 120;
                        }
                        if (Main.player[projectile.owner].statMana < Main.player[projectile.owner].statManaMax) {
                            n.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apExtraManaTick = 120;
                        }
                        Main.npc[n.whoAmI].netUpdate = true;
                    }
                }
                return;
            }

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher) {
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
                AmmoboxHelpfulMethods.chaseEnemy(projectile.identity, projectile.type);
            }

            //  Common for all launchers
            /* 
                        Do stuff here
            */

        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;
            projectile.velocity = Vector2.Zero;
            drawAura = true;

            return false;
        }

        public override void Kill(int timeLeft) {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;
            for(int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 12);
            }
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, projectile.identity, projectile.type, skipDamage: true);
        }
    }
}