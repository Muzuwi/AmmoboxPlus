using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles
{
    public class RocketSand : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandy Rocket");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.aiStyle = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.timeLeft = 600;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
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
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 31);
                Main.dust[dust.dustIndex].velocity *= 0.05f;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            if (Main.rand.Next(4) == 0 && !target.boss)
            {
                if (Main.netMode == 0)
                {
                    target.AddBuff(ModContent.BuffType<Buffs.CloudedVision>(), 400);
                }
                else
                {
                    var packet = Mod.GetPacket();
                    int buffType = ModContent.BuffType<Buffs.CloudedVision>();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                    packet.Write(target.whoAmI);
                    packet.Write(buffType);
                    packet.Write(400);
                    packet.Send();
                }
            }
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

        public override void Kill(int timeLeft)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, 8, 8, DustID.Smoke, newColor: Color.LightYellow, Scale: 2f);
            }
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type);
        }
    }
}