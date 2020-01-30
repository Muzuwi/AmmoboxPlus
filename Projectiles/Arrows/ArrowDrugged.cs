using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class ArrowDrugged : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Drugged Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.alpha = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.type == NPCID.TargetDummy) return;
            if (target.realLife != -1) {
                //  Apply only to the parent
                if (Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus) {
                    Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0) { //  Singleplayer
                        Main.npc[target.realLife].DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    } else { //  Sync with others
                        var packet = mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(Main.npc[target.realLife].whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0) {
                    Main.npc[target.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 1000;
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Drugged>(), 500);
                }

            } else {
                //  Normal enemy, do things as usual
                if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus) {
                    target.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0) { //  Singleplayer
                        target.DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    } else { //  Sync with others
                        var packet = mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(target.whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0) {
                    target.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 1000;
                    target.AddBuff(ModContent.BuffType<Buffs.Drugged>(), 500);
                }
            }
        }

        public override void AI() {
            Lighting.AddLight(projectile.Top, Color.MediumPurple.ToVector3());
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}