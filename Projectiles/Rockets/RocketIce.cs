using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketIce : AbstractRocket
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
            Effect = EffectIce.Instance;
            Projectile.width = 8;
            Projectile.height = 8;
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(Projectile.Center, Color.SkyBlue.ToVector3());
            base.AI();
        }
    }
}