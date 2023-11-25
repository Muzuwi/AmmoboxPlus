using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using AmmoboxPlus.NPCs;
using AmmoboxPlus.Projectiles.Abstract;

namespace AmmoboxPlus.Projectiles
{
    public class DartYang : AbstractDart
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
            Projectile.light = 0.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            hit.Damage = 1;
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apYing && target.GetGlobalNPC<AmmoboxGlobalNPC>().apYiYaTick > 0)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, 150, 1, Owner: Projectile.owner);
                }
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
    }
}