using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartSand : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sandy Dart");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.spriteDirection = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if(Main.netMode == 0) {
                target.AddBuff(ModContent.BuffType<Buffs.CloudedVision>(), 300);
            } else {
                var packet = mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.CloudedVision>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(300);
                packet.Send();
            }
        }

        public override void AI() {
            for (int i = 0; i < 2; i++) {
                Dust.NewDust(projectile.position, 1, 1, DustID.Sandstorm);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}