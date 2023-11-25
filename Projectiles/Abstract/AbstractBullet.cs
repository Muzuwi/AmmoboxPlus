using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract
{
    /**
     *  Abstract projectile defining the behavior of a bullet
     */
    public abstract class AbstractBullet : ModProjectile
    {
        public IEffect Effect = null;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Peculiar Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 1;
            Projectile.spriteDirection = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Effect?.Proc(this, target);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }
    }
}
