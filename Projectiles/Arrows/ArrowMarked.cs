using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class ArrowMarked : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Marker Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.alpha = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apMarked = true;

            if (Main.netMode == 0) {
                target.AddBuff(mod.BuffType<Buffs.Marked>(), 100);
            }
            else {
                var packet = mod.GetPacket();
                int buffType = mod.BuffType<Buffs.Marked>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(100);
                packet.Send();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI() {
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width / 2, projectile.height / 2, 90, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f, newColor: Color.Red);
            }
        }
    }
}