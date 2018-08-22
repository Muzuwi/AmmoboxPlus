using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartYang : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Yang Dart");
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
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYing && target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick > 0 && (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYingOwner != projectile.owner) ) {
                Projectile.NewProjectile(target.position, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, 150, 1, Owner: projectile.owner);
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYing = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYang = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick = 0;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYangOwner = -1;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYingOwner = -1;
            } else {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYing = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYang = true;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYiYaTick = 100;
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apYangOwner = projectile.owner;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}