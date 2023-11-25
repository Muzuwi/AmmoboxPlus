using AmmoboxPlus.Projectiles.Abstract;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowSpectral : AbstractArrow
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.scale = 1.2f;
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
                return false;
            }

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
            return false;
        }

        public override void AI()
        {
            base.AI();
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