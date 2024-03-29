using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract
{
    /**
     *  Abstract projectile defining the behavior of a rocket
     */
    public abstract class AbstractRocket : ModProjectile
    {
        public IEffect Effect = null;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunny Rocket");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Effect?.Proc(this, target);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher)
            {
                Projectile.Kill();
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher)
            {
                if (Projectile.ai[1] > 3)
                {
                    Projectile.velocity = Vector2.Zero;
                }
                else
                {
                    Projectile.ai[1] += 1;
                }
            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon)
            {
                Projectile.Kill();
            }

            return true;
        }

        public override void AI()
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher)
            {
                Projectile.velocity = Projectile.oldVelocity;
                if (Math.Abs(Projectile.velocity.X) < 15f && Math.Abs(Projectile.velocity.Y) < 15f) Projectile.velocity *= 1.1f;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }

            //  Grenade launcher
            if (shotFrom == ItemID.GrenadeLauncher)
            {
                //Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                //  If going to explode 
                if (Projectile.ai[1] == 200)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.ai[1] += 1;
                }
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher)
            {
                if (Projectile.ai[1] < 3)
                {
                    Projectile.velocity *= 0.98f;
                }
                if (Projectile.ai[1] >= 3 && Projectile.alpha < 150)
                {
                    Projectile.alpha += 1;
                }

            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon)
            {
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                AmmoboxHelpfulMethods.chaseEnemy(Projectile.identity, Projectile.type);
            }

            //  Common for all launchers
            Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.Silver, Projectile.velocity * 0.2f);
            dust.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type);
        }

    }
}
