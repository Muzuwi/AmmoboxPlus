using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.Projectiles.Abstract;
using Microsoft.Xna.Framework;

namespace AmmoboxPlus.Projectiles
{
    public class RocketBlackhole : AbstractRocket
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
            modifiers.FinalDamage *= 0f;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, Mod.Find<ModProjectile>("BlackHole").Type, 0, 0, Projectile.owner);
            }
            if (Main.netMode != NetmodeID.Server)
            {
                SoundStyle style = new SoundStyle("AmmoboxPlus/Sounds/Custom/blackHole");
                SoundEngine.PlaySound(style, Projectile.position);
            }
            base.OnKill(timeLeft);
        }
    }
}