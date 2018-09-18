using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartYing : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ying Dart");
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
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.spriteDirection = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYang && target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick > 0) {
                Projectile.NewProjectile(target.position, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, 150, 1, Owner: projectile.owner);
                Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYing = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYang = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick = 0;
            } else {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYing = true;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYang = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick = 300;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            damage = 2;
        }
    }
}