using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class BulletMarked : AbstractBullet
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectMarked.Instance;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, newColor: Color.Red);
            }
        }
    }
}