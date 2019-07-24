using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;
using AmmoboxPlus;

namespace AmmoboxPlus.Projectiles {
    public class DartBunny : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Peculiar Dart");
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
            projectile.spriteDirection = 1;

            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.boss || target.type == NPCID.TargetDummy ) {
                return;
            }
            if (WorldGen.genRand.Next(10) == 0) {
                if(Main.netMode == 0) {
                    Vector2 pos = target.position;
                    target.active = false;
                    NPC.NewNPC((int)pos.X, (int)pos.Y, NPCID.Bunny);
                    Main.PlaySound(SoundID.DoubleJump, pos);
                    for (int i = 0; i < 20; i++) {
                        Dust.NewDust(target.position, 16, 16, DustID.Confetti);
                    }

                } else {
                    for (int i = 0; i < 20; i++) {
                        Dust.NewDust(target.position, 16, 16, DustID.Confetti);
                    }
                    var packet = mod.GetPacket();
                    packet.Write((byte)AmmoboxMsgType.AmmoboxBunny);
                    packet.Write(target.whoAmI);
                    packet.Send();
                }
            }
        }

        public override void AI() {
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Silver, newColor: new Color(255,255,255));
            }
            Lighting.AddLight(projectile.Top, Color.White.ToVector3());
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}