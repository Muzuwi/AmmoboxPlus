using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketCursed : AbstractRocket
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cursed Rocket");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Effect = EffectCursed.Instance;
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
            Lighting.AddLight(Projectile.position, Color.Lime.ToVector3());
        }

        public override void OnKill(int timeLeft)
        {
            AmmoboxHelpfulMethods.createBurst(ProjectileID.CursedFlameFriendly, Projectile.position, Projectile.owner, 5, Count: 2, oneInX: 1);
            base.OnKill(timeLeft);
        }
    }
}