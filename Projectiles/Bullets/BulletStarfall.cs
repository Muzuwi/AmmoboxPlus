using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletStarfall : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Starfall Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 600;
            projectile.alpha = 1;
            projectile.scale = 1.6f;
            projectile.spriteDirection = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            aiType = ProjectileID.Bullet;
        }

        public override void AI() {
            //  Check if projectile just spawned
            if(WorldGen.genRand.Next(10) == 0 && projectile.timeLeft == 600) {
                Vector2 vel = projectile.velocity, pos = projectile.position;
                int own = projectile.owner;
                projectile.Kill();
                int id = Projectile.NewProjectile(pos, vel, ProjectileID.FallingStar, 40, 0, own);
                Main.projectile[id].friendly = true;
            } else {
                //Dust.NewDust(projectile.position, projectile.width/2, projectile.height/2, 214);
            }
            Lighting.AddLight(projectile.Top, Color.Yellow.ToVector3());
        }
        

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}