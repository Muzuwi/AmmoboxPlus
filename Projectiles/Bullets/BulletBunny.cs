using Microsoft.Xna.Framework;
using Terraria;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class BulletBunny : AbstractBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectBunny.Instance;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Top, Color.White.ToVector3());
        }

    }
}