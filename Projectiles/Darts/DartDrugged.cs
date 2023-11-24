using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class DartDrugged : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drugged Dart");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
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
            Projectile.light = 0f;
            Projectile.spriteDirection = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            if (target.type == NPCID.TargetDummy) return;
            if (target.realLife != -1)
            {
                //  Apply only to the parent
                if (Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus)
                {
                    Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0)
                    { //  Singleplayer
                        Main.npc[target.realLife].DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    }
                    else
                    { //  Sync with others
                        var packet = Mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(Main.npc[target.realLife].whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0)
                {
                    Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 1000;
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Drugged>(), 500);
                }

            }
            else
            {
                //  Normal enemy, do things as usual
                if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus)
                {
                    target.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0)
                    { //  Singleplayer
                        target.DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    }
                    else
                    { //  Sync with others
                        var packet = Mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(target.whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0)
                {
                    target.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 900;
                    target.AddBuff(ModContent.BuffType<Buffs.Drugged>(), 600);
                }
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