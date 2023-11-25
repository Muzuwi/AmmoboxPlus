using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class DartBunny : AbstractDart
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectBunny.Instance;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, newColor: new Color(255, 255, 255));
            Lighting.AddLight(Projectile.Top, Color.White.ToVector3());
        }
    }
}