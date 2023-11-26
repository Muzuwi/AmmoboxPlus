using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class BulletMiner : AbstractBullet
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectMiner.Instance;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Dirt, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
        }
    }
}