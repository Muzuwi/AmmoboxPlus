using AmmoboxPlus.Projectiles.Abstract;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace AmmoboxPlus.Projectiles
{
    public class BulletSpectral : AbstractBullet
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 4;
            Projectile.height = 4;
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
            // FIXME: Re-add
            lightColor = Color.White;
            //Redraw the projectile with the color not influenced by light
            // Vector2 drawOrigin = new Vector2(Main.projectileTexture[Projectile.type].Width * 0.5f, Projectile.height * 0.5f);
            // for (int k = 0; k < Projectile.oldPos.Length; k++)
            // {
            //     Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            //     Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.3f;
            //     spriteBatch.Draw(Main.projectileTexture[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            // }
            return true;
        }
    }
}