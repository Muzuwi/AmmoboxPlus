using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class RocketStarburst : AbstractRocket
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void AI()
        {
            base.AI();
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            if (!(shotFrom == ItemID.ProximityMineLauncher && Projectile.ai[1] >= 3))
            {
                Dust.NewDustPerfect(Projectile.Center - new Vector2(0, Projectile.height / 2), DustID.GoldCoin);
            }
            Lighting.AddLight(Projectile.Center + new Vector2(0, Projectile.height / 2), Color.Yellow.ToVector3());
        }

        public override void OnKill(int timeLeft)
        {
            AmmoboxHelpfulMethods.createBurst(ProjectileID.FallingStar, Projectile.position, Projectile.owner, 40);
            base.OnKill(timeLeft);
        }
    }
}