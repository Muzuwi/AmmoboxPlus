using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class DartFrostburn : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frostburn Dart");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.spriteDirection = 1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            target.AddBuff(BuffID.Frostburn, 200);
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Dust.NewDust(Projectile.position, 1, 1, 20);
            }
            Lighting.AddLight(Projectile.position, Color.DeepSkyBlue.ToVector3());
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }
    }
}