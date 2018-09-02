using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletDrugged : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Drugged Bullets");
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
            projectile.scale = 2f;
            projectile.spriteDirection = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
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
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedCooldown = 1000;
                target.AddBuff(mod.BuffType<Buffs.Drugged>(), 500);
            }            
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI() {
            Lighting.AddLight(projectile.Top, Color.MediumPurple.ToVector3());
        }

    }
}