using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketIchor : AbstractRocket
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
            Effect = EffectIchor.Instance;
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch, 0f, 0f, 100);
            Lighting.AddLight(Projectile.Center, Color.LightYellow.ToVector3());
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
            AmmoboxHelpfulMethods.createBurst(ProjectileID.IchorSplash, Projectile.position, Projectile.owner, 5, Count: 2, oneInX: 1);
        }
    }
}