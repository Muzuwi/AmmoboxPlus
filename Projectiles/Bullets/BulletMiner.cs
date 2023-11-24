using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class BulletMiner : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Miner's Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.light = 0.5f;
            Projectile.scale = 2f;
            Projectile.spriteDirection = 1;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre) return;
            if (AmmoboxHelpfulMethods.processMinerOreDrop(target))
            {
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre = true;
            }
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Dirt, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }
    }
}