using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowTrueChloro : AbstractArrow
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

        public override void AI()
        {
            base.AI();
            // Adapted from vanilla, chlorophyte-like trail effect
            for (int idx = 0; idx < 10; ++idx)
            {
                Vector2 position = new Vector2(
                    (float)(Projectile.position.X - Projectile.velocity.X / 10.0 * idx),
                    (float)(Projectile.position.Y - Projectile.velocity.Y / 10.0 * idx)
                );
                int dustIdx = Dust.NewDust(position, 1, 1, DustID.CursedTorch, 0.0f, 0.0f, 0, Color.Lime, 1f);
                Main.dust[dustIdx].alpha = Projectile.alpha;
                Main.dust[dustIdx].position = position;
                Main.dust[dustIdx].velocity = new Vector2(0, 0);
                Main.dust[dustIdx].noGravity = true;
            }
            AmmoboxHelpfulMethods.chaseEnemy(Projectile.identity, Projectile.type);
        }
    }
}