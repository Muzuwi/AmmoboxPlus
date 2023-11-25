using Terraria;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class DartCactus : AbstractDart
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 8;
            Projectile.height = 8;
        }
    }
}