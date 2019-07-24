using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AmmoboxPlus.Projectiles {
    public class ArrowTrueChloro : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("True Chlorophyte Arrow");
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
            projectile.extraUpdates = 1;
        }

        public override void AI() {

            //  Adapted from vanilla
            //  Variable names might not be true to their function

            for (int index1 = 0; index1 < 10; ++index1) {
                float num1 = (float)(projectile.position.X - projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(projectile.position.Y - projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 75, 0.0f, 0.0f, 0, Color.Lime, 1f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0,0);
                Main.dust[index2].noGravity = true;
            }

            AmmoboxHelpfulMethods.chaseEnemy(projectile.identity, projectile.type);

        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}