using AmmoboxPlus.Projectiles.Abstract;
using Terraria;
using Terraria.ID;

namespace AmmoboxPlus.Projectiles
{
    public class BulletRubber : AbstractBullet
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
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hit.Knockback = 15;
        }
    }
}