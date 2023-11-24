using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowTrueChloro : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Chlorophyte Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {

            //  Adapted from vanilla
            //  Variable names might not be true to their function

            for (int index1 = 0; index1 < 10; ++index1)
            {
                float num1 = (float)(Projectile.position.X - Projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(Projectile.position.Y - Projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 75, 0.0f, 0.0f, 0, Color.Lime, 1f);
                Main.dust[index2].alpha = Projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }

            AmmoboxHelpfulMethods.chaseEnemy(Projectile.identity, Projectile.type);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }

    }
}