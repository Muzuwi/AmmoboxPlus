using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class RocketHeart : ModProjectile
    {

        public bool drawAura = false;
        public double rotation = 0d;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Regeneration Rocket");
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

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            if (drawAura)
            {
                if (rotation >= 2)
                {
                    rotation = 0;
                }
                else
                {
                    rotation += (1 / 256d);
                }

                // (0, 142, 255)
                //  0, 0x11, 255
                AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 178, 256, true, true, 32, color: new Color(0, 142, 255), angleOffset: rotation, shader: 39);
                AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 178, 256, true, true, 32, color: new Color(255, 0, 0), angleOffset: (1 / 32d), shader: 39);
                foreach (NPC n in Main.npc)
                {
                    float a = Math.Abs(Projectile.Center.X - n.Center.X);
                    float b = Math.Abs(Projectile.Center.Y - n.Center.Y);
                    double dist = Math.Sqrt(a * a + b * b);

                    if (dist < 192f && n.active && !n.friendly)
                    {
                        if (Main.player[Projectile.owner].statLife < Main.player[Projectile.owner].statLifeMax)
                        {
                            n.GetGlobalNPC<AmmoboxGlobalNPC>().apExtraHeartTick = 120;
                        }
                        if (Main.player[Projectile.owner].statMana < Main.player[Projectile.owner].statManaMax)
                        {
                            n.GetGlobalNPC<AmmoboxGlobalNPC>().apExtraManaTick = 120;
                        }
                        Main.npc[n.whoAmI].netUpdate = true;
                    }
                }
                return;
            }

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
            /* 
                        Do stuff here
            */

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            Projectile.velocity = Vector2.Zero;
            drawAura = true;

            return false;
        }

        public override void Kill(int timeLeft)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 12);
            }
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type, skipDamage: true);
        }
    }
}