using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class DartStarfall : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starfall Dart");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
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
            Projectile.timeLeft = 600;
            Projectile.alpha = 1;
            Projectile.spriteDirection = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            //  Check if projectile just spawned
            if (WorldGen.genRand.Next(10) == 0 && Projectile.timeLeft == 600)
            {
                Vector2 vel = Projectile.velocity, pos = Projectile.position;
                int own = Projectile.owner;
                Projectile.Kill();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos, vel, ProjectileID.FallingStar, 40, 0, own);
            }
            else
            {
                Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, 214);
                Lighting.AddLight(Projectile.position, Color.LightYellow.ToVector3());
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }
    }
}