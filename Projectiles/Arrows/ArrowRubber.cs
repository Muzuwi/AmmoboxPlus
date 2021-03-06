using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles {
    public class ArrowRubber : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rubber Arrow");
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
            projectile.spriteDirection = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            knockback = 15;
        }


        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}