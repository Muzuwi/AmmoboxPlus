using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowSlime : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime Arrow");
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
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;
            target.GetGlobalNPC<AmmoboxGlobalNPC>().apSlime = true;

            if (Main.netMode == 0)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Slime>(), 200);
                target.AddBuff(BuffID.Slimed, 200);
            }
            else
            {
                target.AddBuff(BuffID.Slimed, 200);
                var packet = Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Slime>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxSlime);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(200);
                packet.Send();
            }

        }

        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDust(Projectile.position, 1, 1, DustID.PinkSlime, newColor: Color.DeepSkyBlue);
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