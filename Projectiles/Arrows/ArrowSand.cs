using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;
using Terraria;
using Terraria.ID;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowSand : AbstractArrow
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectSand.Instance;
        }

        public override void AI()
        {
            base.AI();
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDust(Projectile.position, 1, 1, DustID.Sandstorm);
            }
        }
    }
}