using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;

namespace AmmoboxPlus.Projectiles {
    public class ArrowSpectral : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Spectral Arrow");
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
            projectile.alpha = 1;
            projectile.light = 0f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.maxPenetrate = 5;
            projectile.penetrate = 6;
            projectile.spriteDirection = 1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            projectile.velocity = oldVelocity;
            --projectile.penetrate;
            if (projectile.penetrate <= 0) {
                projectile.Kill();
            }
            else {
                //  Calculate """""angle"""""
                double tng = projectile.velocity.Y / projectile.velocity.X;
                double angle = Math.Atan(tng);
                if (angle < -1.2) {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                }
                else if (angle < 0) {
                    projectile.velocity.Y += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                }
                else if (angle < 1.2) {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                }
                else {
                    projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (projectile.penetrate * 1.12f), projectile.penetrate * 1.12f);
                }

            }
            return false;
        }
    }
}