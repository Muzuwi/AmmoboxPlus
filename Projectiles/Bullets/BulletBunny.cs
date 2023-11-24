using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;
using AmmoboxPlus;


namespace AmmoboxPlus.Projectiles
{
    public class BulletBunny : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Peculiar Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 1;
            Projectile.scale = 2f;
            Projectile.spriteDirection = 1;

            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            if (target.boss || target.type == NPCID.TargetDummy)
            {
                return;
            }
            if (WorldGen.genRand.Next(10) == 0)
            {
                if (Main.netMode == 0)
                {
                    Vector2 pos = target.position;
                    target.active = false;
                    NPC.NewNPC(Projectile.GetSource_FromThis(), (int)pos.X, (int)pos.Y, NPCID.Bunny);
                    SoundEngine.PlaySound(SoundID.DoubleJump, pos);
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(target.position, 16, 16, DustID.Confetti);
                    }

                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(target.position, 16, 16, DustID.Confetti);
                    }
                    var packet = Mod.GetPacket();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxBunny);
                    packet.Write(target.whoAmI);
                    packet.Send();
                }
            }
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, newColor: new Color(255,255,255));
            }
            Lighting.AddLight(Projectile.Top, Color.White.ToVector3());
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }
    }
}