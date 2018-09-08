using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class RocketBlackhole : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Blackhole Rocket");
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

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            damage = 0;
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
            /* 
                        Do stuff here
            */

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
            int id = Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType("BlackHole"), 0, 0, projectile.owner);
            if(Main.netMode == 1) {
                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, id);
            }
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, projectile.identity);
        }
    }
}