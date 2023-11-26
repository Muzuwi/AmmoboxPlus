using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class RocketHarpy : AbstractRocket
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
            Projectile.width = 14;
            Projectile.height = 20;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
            AmmoboxHelpfulMethods.createBurst(ProjectileID.HarpyFeather, Projectile.position, Projectile.owner, 20, oneInX: 1, makeFriendly: true);
        }
    }
}