using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;
using AmmoboxPlus.Projectiles.Abstract.Effects;

namespace AmmoboxPlus.Projectiles
{
    public class RocketMiner : AbstractRocket
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
            Effect = EffectMiner.Instance;
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, Color.Brown.ToVector3());
        }

        public override void OnKill(int timeLeft)
        {
            // Don't call base - custom behavior
            AmmoboxHelpfulMethods.blastArea(Projectile.position, maxRange: 7, extraDrops: true);
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type, largeBlast: true);
        }
    }
}