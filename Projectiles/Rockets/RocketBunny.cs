using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketBunny : AbstractRocket
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectBunny.Instance;
            Projectile.width = 8;
            Projectile.height = 8;
        }
    }
}