using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class DartDrugged : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Drugged Dart");
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
            projectile.light = 0f;
            projectile.spriteDirection = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.realLife != -1) return;
            if (target.type == NPCID.TargetDummy) return;
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCactus) {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCactus = false;
                if (Main.netMode == 0) { //  Singleplayer
                    target.DelBuff(mod.BuffType<Buffs.Cactus>());
                } else { //  Sync with others
                    var packet = mod.GetPacket();
                    int buffType = mod.BuffType<Buffs.Cactus>();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                    packet.Write(target.whoAmI);
                    packet.Write(buffType);
                    packet.Send();
                }
            }

            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedCooldown == 0) {
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedCooldown = 900;
                target.AddBuff(mod.BuffType<Buffs.Drugged>(), 600);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

    }
}