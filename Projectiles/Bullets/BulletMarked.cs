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
            projectile.width = 8;
            projectile.height = 8;
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
            //ErrorLogger.Log("Enemy id " + target.type + " hit with marked, applying debuff");
            target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apMarked = true;
            if(Main.netMode == 0) {
                target.AddBuff(mod.BuffType<Buffs.Marked>(), 100);  
            }else if(Main.netMode == 1 || Main.netMode == 2) { //  Does it actually work this way?
                NetMessage.SendData(0x35, -1, -1, Terraria.Localization.NetworkText.Empty, target.whoAmI, mod.BuffType<Buffs.Marked>(), 100);
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++) {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

    }
}