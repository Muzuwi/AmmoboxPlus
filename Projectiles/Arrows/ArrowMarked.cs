using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowMarked : AbstractArrow
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
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width / 2, Projectile.height / 2, DustID.GemRuby, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, newColor: Color.Red);
        }
    }
}