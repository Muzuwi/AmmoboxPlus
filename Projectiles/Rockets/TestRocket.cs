using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class TestRocket : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Test Rocket");
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
            //  OH BOY
            Vector2 initialPos = projectile.position;
            float radius = 10;
            float multi = 1.1f;

            for (float i = 0f; i < 2; i += 0.125f) {
                if( (i >= 0 && i <= 0.5) || (i >= 1 && i <= 1.5) ) {
                    double x = radius * Math.Cos(i * Math.PI);
                    double y = radius * Math.Sin(i * Math.PI);
                    Vector2 vel = new Vector2((float)x * multi, (float)y * multi);
                    Projectile.NewProjectile(initialPos + new Vector2((float)x, (float)y), vel, ProjectileID.FallingStar, 40, 0, projectile.owner); 
                }
                else if ( (i >= 0.5 && i <= 1) || (i >= 1.5 && i < 2) ) {
                    double y = radius * Math.Cos(i * Math.PI);
                    double x = radius * Math.Sin(i * Math.PI);
                    Vector2 vel = new Vector2((float)x * multi, (float)y * multi);
                    Projectile.NewProjectile(initialPos + new Vector2((float)x, (float)y), vel, ProjectileID.FallingStar, 40, 0, projectile.owner);
                }
            }
        }

        public override void AI() {
            Dust.NewDustPerfect(projectile.Center - new Vector2(0, projectile.height/2), DustID.GoldCoin);
            Lighting.AddLight(projectile.Center + new Vector2(0, projectile.height/2), Color.Yellow.ToVector3());
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}