using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class DartMarked : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Marker Dart");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 1;
            Projectile.spriteDirection = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            target.GetGlobalNPC<AmmoboxGlobalNPC>().apMarked = true;

            if (Main.netMode == 0)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Marked>(), 100);
            }
            else
            {
                var packet = Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Marked>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(100);
                packet.Send();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width / 2, Projectile.height / 2, 90, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, newColor: Color.Red);
            }
        }
    }
}