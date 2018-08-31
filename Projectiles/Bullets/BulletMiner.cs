using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles {
    public class BulletMiner : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Miner's Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 1;
            projectile.light = 0.5f;
            projectile.scale = 2f;
            projectile.spriteDirection = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (WorldGen.genRand.Next(100) != 0) return;
            if (target.type == NPCID.TargetDummy) return;
            if (target.SpawnedFromStatue) return;
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre) return;

            //  Load drop table
            var dropTable = new WeightedRandom<int>();
            if (!Main.hardMode) {
                foreach (int a in AmmoboxPlus.AmmoboxOreVanillaPHM) {
                    dropTable.Add(a);
                }
            } else if (Main.hardMode) {
                //  Include both phm and hm ores
                foreach (int a in AmmoboxPlus.AmmoboxOreVanillaPHM) {
                    dropTable.Add(a);
                }
                if(AmmoboxPlus.AmmoboxOreVanillaHM.Count > 0) {
                    foreach (int a in AmmoboxPlus.AmmoboxOreVanillaHM) {
                        dropTable.Add(a);
                    }
                }
            }

            //  Add mod-supplied ores to the drop table (if any exist)
            if (AmmoboxPlus.AmmoboxOreModded.Count > 0) {
                foreach (int a in AmmoboxPlus.AmmoboxOreModded) {
                    dropTable.Add(a);
                }
            }

            //  Spawn the item
            int index = Item.NewItem(target.position, dropTable, WorldGen.genRand.Next(10, 30));
            if(Main.netMode == 1) {
                NetMessage.SendData(21, -1, -1, Terraria.Localization.NetworkText.Empty, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre = true;

        }

        public override void AI() {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Dirt, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}