using AmmoboxPlus.Projectiles.Abstract;
using Terraria;
using Terraria.ID;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowRubber : AbstractArrow
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
            Projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hit.Knockback = 15;
        }
    }
}