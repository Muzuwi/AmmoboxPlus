using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using AmmoboxPlus.NPCs;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class RocketHeart : AbstractRocket
    {
        public bool drawAura = false;
        public double rotation = 0d;

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

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            base.AI();

            if (!drawAura)
            {
                return;
            }

            if (rotation >= 2)
            {
                rotation = 0;
            }
            else
            {
                rotation += 1 / 256d;
            }

            // (0, 142, 255)
            //  0, 0x11, 255
            AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 178, 256, true, true, 32, color: new Color(0, 142, 255), angleOffset: rotation, shader: 39);
            AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 178, 256, true, true, 32, color: new Color(255, 0, 0), angleOffset: (1 / 32d), shader: 39);

            // FIXME: WHY
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            drawAura = true;
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            // Don't call base - we need to override default explode damage behavior

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HeartCrystal);
            }
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;
            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type, skipDamage: true);
        }
    }
}