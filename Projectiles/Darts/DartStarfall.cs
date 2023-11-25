using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class DartStarfall : AbstractDart
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Top, Color.Yellow.ToVector3());

            // Check if projectile just spawned
            if (Projectile.timeLeft != 600)
            {
                return;
            }

            // Chance to spawn a star
            if (!WorldGen.genRand.NextBool(10))
            {
                return;
            }

            Projectile.Kill();
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vel = Projectile.velocity;
                Vector2 pos = Projectile.position;
                int own = Projectile.owner;
                int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos, vel, ProjectileID.FallingStar, 40, 0, own);
                Main.projectile[id].friendly = true;
            }
        }
    }
}