using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartStarfall : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Starfall Dart");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 600;
            projectile.alpha = 1;
            projectile.spriteDirection = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI() {
            //  Check if projectile just spawned
            if(WorldGen.genRand.Next(10) == 0 && projectile.timeLeft == 600) {
                Vector2 vel = projectile.velocity, pos = projectile.position;
                int own = projectile.owner;
                projectile.Kill();
                Projectile.NewProjectile(pos, vel, ProjectileID.FallingStar, 40, 0, own);
            } else {
                Dust.NewDust(projectile.position, projectile.width/2, projectile.height/2, 214);
                Lighting.AddLight(projectile.position, Color.LightYellow.ToVector3());
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}