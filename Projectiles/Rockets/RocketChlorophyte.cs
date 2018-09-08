using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class RocketChlorophyte : ModProjectile {

        public int tickAliveCount = 0;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Chlorophyte Rocket");
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

        public override void AI() {
            int shotFrom = projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>(mod).apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher) {
                projectile.velocity = projectile.oldVelocity;
                if (Math.Abs(projectile.velocity.X) < 15f && Math.Abs(projectile.velocity.Y) < 15f) projectile.velocity *= 1.1f;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                AmmoboxHelpfulMethods.chaseEnemy(projectile.identity);
            }

            //  Grenade launcher
            if (shotFrom == ItemID.GrenadeLauncher) {
                //projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                //  If going to explode 
                if (tickAliveCount == 200) {
                    projectile.Kill();
                } else {
                    ++tickAliveCount;
                }
                AmmoboxHelpfulMethods.chaseEnemy(projectile.identity);
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

            for (int index1 = 0; index1 < 10; ++index1) {
                float num1 = (float)(projectile.Center.X - projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(projectile.Center.Y - projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 75, 0.0f, 0.0f, 0, Color.Lime, 1f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }

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
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, projectile.identity);
        }
    }
}