using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class ArrowCactus : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cactus Arrow");
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

            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apDrugged)
            {
                return;
            }

            target.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = true;
            if (Main.netMode == 0)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Cactus>(), 300);
            }
            else
            {
                var packet = Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Cactus>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxCactus);
                packet.Write(target.whoAmI);
                packet.Write(buffType);
                packet.Write(300);
                packet.Send();
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