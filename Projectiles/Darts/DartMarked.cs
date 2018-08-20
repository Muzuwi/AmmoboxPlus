using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartMarked : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Marker Dart");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.alpha = 1;
            projectile.light = 0f;
            projectile.scale = 1f;
            projectile.spriteDirection = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apMarked = true;
            if (Main.netMode == 0) {
                target.AddBuff(mod.BuffType<Buffs.Marked>(), 100);
            }
            else if (Main.netMode == 1 || Main.netMode == 2) { //  Does it actually work this way?
                NetMessage.SendData(0x35, -1, -1, Terraria.Localization.NetworkText.Empty, target.whoAmI, mod.BuffType<Buffs.Marked>(), 100);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        /*public override void AI() {
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 90, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f, newColor: Color.Red);
            }
        }*/
    }
}