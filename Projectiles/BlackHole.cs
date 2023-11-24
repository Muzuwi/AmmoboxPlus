using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles
{
    class BlackHole : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Hole");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.timeLeft = 300;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Main.projFrames[Projectile.type] = 5;
        }

        public override void AI()
        {
            Projectile.frame = (Projectile.frame == 4 ? 0 : ++Projectile.frame);

            if (Projectile.timeLeft <= 60)
            {
                Projectile.alpha += 255 / 60;
            }
            AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 199, 128, true, true, 64, new Color(255, 255, 255), velocity: new Vector2(0f, 0f));
            AmmoboxHelpfulMethods.createDustCircle(Projectile.Center, 211, noGravity: true, radius: 16, newDustPerfect: true, Count: 16, velocity: new Vector2(0, 0), color: new Color(255, 0, 251));
            //  Do black holey stuff
            Projectile.velocity = Vector2.Zero;

            double maxRange = 128;
            foreach (NPC npc in Main.npc)
            {
                double x = Projectile.Center.X - npc.Center.X;
                double y = Projectile.Center.Y - npc.Center.Y;
                double distance = Vector2.Distance(Projectile.Center, npc.Center);
                if (distance < maxRange && npc.active && !npc.boss)
                {
                    Vector2 vel = new Vector2((float)x, (float)y) * 0.04f;
                    npc.velocity = vel;
                    if (Main.netMode == 1)
                    {
                        var packet = Mod.GetPacket();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxUpdateVelocity);
                        packet.Write(npc.whoAmI);
                        packet.WriteVector2(vel);
                        packet.Send();
                    }
                }
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }


    }
}
