using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketSand : AbstractRocket
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
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void AI()
        {
            base.AI();
            Dust dust = Dust.NewDustPerfect(Projectile.Center, 31);
            Main.dust[dust.dustIndex].velocity *= 0.05f;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, 8, 8, DustID.Smoke, newColor: Color.LightYellow, Scale: 2f);
            }
        }
    }
}