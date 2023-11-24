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
    public class DartYang : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yang Dart");
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

            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apYing && target.GetGlobalNPC<AmmoboxGlobalNPC>().apYiYaTick > 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, 150, 1, Owner: Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYing = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYang = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYiYaTick = 0;
            }
            else
            {
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYing = false;
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYang = true;
                target.GetGlobalNPC<AmmoboxGlobalNPC>().apYiYaTick = 300;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 0.01f;
        }
    }
}