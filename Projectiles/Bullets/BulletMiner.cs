using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletMiner : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Miner's Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 4;
            projectile.height = 4;
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
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre) return;
            if (AmmoboxHelpfulMethods.processMinerOreDrop(target)) {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre = true;
            }
        }

        public override void AI() {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Dirt, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}