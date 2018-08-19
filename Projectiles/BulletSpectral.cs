using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles {
    public class BulletSpectral : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Spectral Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = 1.2f;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.alpha = 1;
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.maxPenetrate = 5;
            projectile.penetrate = 6;
            projectile.spriteDirection = 1;

            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            // Preserve old velocity
            projectile.velocity = oldVelocity;

            --projectile.penetrate;
            if(projectile.penetrate <= 0) {
                projectile.Kill();
            } else {
                //  Calculate """""angle"""""
                double tng = projectile.velocity.Y / projectile.velocity.X;
                double angle = Math.Atan(tng);
                if(angle < -1.2) {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1*(projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                } else if(angle < 0) {
                    projectile.velocity.Y += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                } else if(angle < 1.2) {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                } else {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                }

            }
            return false;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++) {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length)*0.3f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}