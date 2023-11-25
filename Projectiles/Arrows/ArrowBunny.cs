using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowBunny : AbstractArrow
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectBunny.Instance;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, newColor: new Color(255, 255, 255));
        }
    }
}