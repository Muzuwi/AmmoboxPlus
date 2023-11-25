using Microsoft.Xna.Framework;
using Terraria;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class BulletDrugged : AbstractBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectDrugged.Instance;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.scale = 2f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Top, Color.MediumPurple.ToVector3());
        }

    }
}