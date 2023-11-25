using Terraria;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class BulletCactus : AbstractBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectCactus.Instance;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = 1f;
        }
    }
}