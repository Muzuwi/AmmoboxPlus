using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletMarked : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Markershot");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.light = 0.5f;
            projectile.scale = 2f;
            projectile.spriteDirection = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apMarked = true;

            if(Main.netMode == 0) {
                target.AddBuff(mod.BuffType<Buffs.Marked>(), 100);  
            } else {
                var packet = mod.GetPacket();
                int buffType = mod.BuffType<Buffs.Marked>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxMarked);
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
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 90, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f, newColor: Color.Red);
            }
        }
    }
}