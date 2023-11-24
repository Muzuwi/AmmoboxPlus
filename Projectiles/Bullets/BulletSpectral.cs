using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles
{
    public class BulletSpectral : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantasmal Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.alpha = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.maxPenetrate = 5;
            Projectile.penetrate = 6;
            Projectile.spriteDirection = 1;
            AIType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Preserve old velocity
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
                    Projectile.velocity.Y += WorldGen.genRand.NextFloat(-1 * (Projectile.penetrate * 1.12f), Projectile.penetrate * 1.12f);
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