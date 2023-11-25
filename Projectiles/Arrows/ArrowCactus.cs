using Terraria;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowCactus : AbstractArrow
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectCactus.Instance;
            Projectile.width = 8;
            Projectile.height = 8;
        }
    }
}