using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletBunny : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Peculiar Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        
        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.alpha = 1;
            projectile.scale = 2f;
            projectile.spriteDirection = 1;

            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.boss || target.type == NPCID.TargetDummy ) {
                return;
            }
            if(WorldGen.genRand.Next(10) == 0) {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apBunny = true;
                target.netUpdate = true;
            }
        }

        public override void AI() {
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Silver, newColor: new Color(255,255,255));
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}