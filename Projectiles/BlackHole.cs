﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles {
    class BlackHole : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Black Hole");
        }

        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.timeLeft = 300;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            Main.projFrames[projectile.type] = 5;
        }

        public override void AI() {
            projectile.frame = (projectile.frame == 4 ? 0 : ++projectile.frame);

            if (projectile.timeLeft <= 60) {
                projectile.alpha += 255 / 60;
            }
            AmmoboxHelpfulMethods.createDustCircle(projectile.Center, 199, 128, true, true, 64, new Color(255, 255, 255), velocity: new Vector2(0f, 0f));
            AmmoboxHelpfulMethods.createDustCircle(projectile.Center, 211, noGravity: true, radius: 16, newDustPerfect: true, Count: 16, velocity: new Vector2(0, 0), color: new Color(255, 0, 251));
            //  Do black holey stuff
            projectile.velocity = Vector2.Zero;

            double maxRange = 128;
            foreach (NPC npc in Main.npc) {
                double x = projectile.Center.X - npc.Center.X;
                double y = projectile.Center.Y - npc.Center.Y;
                double distance = Vector2.Distance(projectile.Center, npc.Center);
                if (distance < maxRange && npc.active && !npc.boss) {
                    Vector2 vel = new Vector2((float)x, (float)y) * 0.04f;
                    npc.velocity = vel;
                    if (Main.netMode == 1) {
                        var packet = mod.GetPacket();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxUpdateVelocity);
                        packet.Write(npc.whoAmI);
                        packet.WriteVector2(vel);
                        packet.Send();
                    }
                }
            }
        }

        public override bool? CanHitNPC(NPC target) {
            return false;
        }


    }
}
