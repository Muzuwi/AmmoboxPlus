using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartAcupuncture : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Acupuncture Dart");
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

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            if (!AmmoboxPlayer.apAcupunctureFirstTarget) {
                AmmoboxPlayer.apAcupunctureTargetID = target.whoAmI;
                AmmoboxPlayer.apAcupunctureFirstTarget = true;
            } else {
                if (AmmoboxPlayer.apAcupunctureTargetID != target.whoAmI) {
                    AmmoboxPlayer.apAcupunctureDmgIncrease = 0;
                    AmmoboxPlayer.apAcupunctureFirstTarget = false;
                } else {
                    AmmoboxPlayer.apAcupunctureDmgIncrease += 2;
                }
            }
            damage += AmmoboxPlayer.apAcupunctureDmgIncrease;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if (AmmoboxPlayer.apAcupunctureFirstTarget) {
                AmmoboxPlayer.apAcupunctureFirstTarget = false;
                AmmoboxPlayer.apAcupunctureTargetID = -1;
                AmmoboxPlayer.apAcupunctureDmgIncrease = 0;
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI() {
            Lighting.AddLight(projectile.position, Color.GhostWhite.ToVector3());
        }

    }
}