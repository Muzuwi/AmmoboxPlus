using AmmoboxPlus.Projectiles.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AmmoboxPlus.Projectiles
{
    public class DartAcupuncture : AbstractDart
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 8;
            Projectile.height = 8;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);

            if (!AmmoboxPlayer.apAcupunctureFirstTarget)
            {
                AmmoboxPlayer.apAcupunctureTargetID = target.whoAmI;
                AmmoboxPlayer.apAcupunctureFirstTarget = true;
                return;
            }

            if (AmmoboxPlayer.apAcupunctureTargetID != target.whoAmI)
            {
                AmmoboxPlayer.apAcupunctureDmgIncrease = 0;
                AmmoboxPlayer.apAcupunctureFirstTarget = false;
            }
            else
            {
                AmmoboxPlayer.apAcupunctureDmgIncrease += 2;
            }

            // FIXME: Isn't this exponential?
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
            return base.OnTileCollide(oldVelocity);
        }

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.position, Color.GhostWhite.ToVector3());
        }

    }
}