using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class RocketGolemfist : AbstractRocket
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

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.Knockback += 20f;
        }
    }
}