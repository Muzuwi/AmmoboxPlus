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
    public class DartAcupuncture : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Acupuncture Dart");
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
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.spriteDirection = 1;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!AmmoboxPlayer.apAcupunctureFirstTarget)
            {
                AmmoboxPlayer.apAcupunctureTargetID = target.whoAmI;
                AmmoboxPlayer.apAcupunctureFirstTarget = true;
            }
            else
            {
                if (AmmoboxPlayer.apAcupunctureTargetID != target.whoAmI)
                {
                    AmmoboxPlayer.apAcupunctureDmgIncrease = 0;
                    AmmoboxPlayer.apAcupunctureFirstTarget = false;
                }
                else
                {
                    AmmoboxPlayer.apAcupunctureDmgIncrease += 2;
                }
            }

            modifiers.FlatBonusDamage += AmmoboxPlayer.apAcupunctureDmgIncrease;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (AmmoboxPlayer.apAcupunctureFirstTarget)
            {
                AmmoboxPlayer.apAcupunctureFirstTarget = false;
                AmmoboxPlayer.apAcupunctureTargetID = -1;
                AmmoboxPlayer.apAcupunctureDmgIncrease = 0;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, Color.GhostWhite.ToVector3());
        }

    }
}