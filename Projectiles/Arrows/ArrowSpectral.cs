using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowSpectral : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantasmal Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.maxPenetrate = 5;
            Projectile.penetrate = 6;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity;
            --Projectile.penetrate;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                //  Calculate """""angle"""""
                double tng = Projectile.velocity.Y / Projectile.velocity.X;
                double angle = Math.Atan(tng);
                if (angle < -1.2)
                {
                    Projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (Projectile.penetrate * 1.12f), Projectile.penetrate * 1.12f);
                }
                else if (angle < 0)
                {
                    Projectile.velocity.Y += WorldGen.genRand.NextFloat(-1 * (Projectile.penetrate * 1.12f), Projectile.penetrate * 1.12f);
                }
                else if (angle < 1.2)
                {
                    Projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (Projectile.penetrate * 1.12f), Projectile.penetrate * 1.12f);
                }
                else
                {
                    Projectile.velocity.X += WorldGen.genRand.NextFloat(-1 * (Projectile.penetrate * 1.12f), Projectile.penetrate * 1.12f);
                }

            }
            return false;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Top, Color.LightBlue.ToVector3());
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // FIXME: Verify if this actually works
            lightColor = Color.Transparent;
            return true;
        }
    }
}