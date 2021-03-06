using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class ArrowSlime : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Slime Arrow");
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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;
            target.GetGlobalNPC<AmmoboxGlobalNPC>().apSlime = true;

            if (Main.netMode == 0) {
                target.AddBuff(ModContent.BuffType<Buffs.Slime>(), 200);
                target.AddBuff(BuffID.Slimed, 200);
            } else {
                target.AddBuff(BuffID.Slimed, 200);
                var packet = mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Slime>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxSlime);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(200);
                packet.Send();
            }

        }

        public override void AI() {
            for (int i = 0; i < 2; i++) {
                Dust.NewDust(projectile.position, 1, 1, DustID.PinkSlime, newColor: Color.DeepSkyBlue);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}