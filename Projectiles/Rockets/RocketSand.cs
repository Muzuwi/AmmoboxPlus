using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class RocketSand : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sandy Rocket");
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
            for(int i = 0; i < 2; i++) {
                Dust dust = Dust.NewDustPerfect(projectile.Center, 31);
                Main.dust[dust.dustIndex].velocity *= 0.05f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if(Main.rand.Next(4) == 0 && !target.boss) {
                if(Main.netMode == 0) {
                    target.AddBuff(mod.BuffType<Buffs.CloudedVision>(), 400);
                } else {
                    var packet = mod.GetPacket();
                    int buffType = mod.BuffType<Buffs.CloudedVision>();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                    packet.Write(target.whoAmI);
                    packet.Write(buffType);
                    packet.Write(400);
                    packet.Send();
                }
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
            for(int i = 0; i < 30; i++) {
                Dust.NewDust(projectile.position, 8, 8, DustID.Smoke, newColor: Color.LightYellow, Scale: 2f);
            }
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, projectile.identity);
        }
    }
}