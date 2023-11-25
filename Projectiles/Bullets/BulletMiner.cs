using Terraria;
using Terraria.ID;
using AmmoboxPlus.NPCs;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class BulletMiner : AbstractBullet
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
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre)
            {
                return;
            }
            target.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre = AmmoboxHelpfulMethods.processMinerOreDrop(target);
        }

        public override void AI()
        {
            base.AI();
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Dirt, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
        }
    }
}