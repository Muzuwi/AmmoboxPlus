using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.timeLeft = 600;
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

            if (target.type == NPCID.TargetDummy) return;
            if (target.SpawnedFromStatue) return;
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre) return;
            //  TODO: Change this into a List<int>, why did i think an array is a good idea
            int[] oreSet;
            if(!Main.hardMode) {
                oreSet = new int[] { ItemID.CopperOre, ItemID.TinOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.DemoniteOre, ItemID.IronOre, ItemID.CrimtaneOre, ItemID.FossilOre };
            } else if (Main.hardMode && !NPC.downedPlantBoss){
                oreSet = new int[] { ItemID.CopperOre, ItemID.TinOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.DemoniteOre, ItemID.IronOre, ItemID.CrimtaneOre, ItemID.FossilOre, ItemID.AdamantiteOre, ItemID.CobaltOre, ItemID.MythrilOre, ItemID.OrichalcumOre, ItemID.PalladiumOre, ItemID.TitaniumOre };
            } else if (NPC.downedPlantBoss && !NPC.downedMoonlord) {
                oreSet = new int[] { ItemID.CopperOre, ItemID.TinOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.DemoniteOre, ItemID.IronOre, ItemID.CrimtaneOre, ItemID.FossilOre, ItemID.AdamantiteOre, ItemID.CobaltOre, ItemID.MythrilOre, ItemID.OrichalcumOre, ItemID.PalladiumOre, ItemID.TitaniumOre, ItemID.ChlorophyteOre };
            } else if (NPC.downedPlantBoss && NPC.downedMoonlord) {
                oreSet = new int[] { ItemID.CopperOre, ItemID.TinOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.DemoniteOre, ItemID.IronOre, ItemID.CrimtaneOre, ItemID.FossilOre, ItemID.AdamantiteOre, ItemID.CobaltOre, ItemID.MythrilOre, ItemID.OrichalcumOre, ItemID.PalladiumOre, ItemID.TitaniumOre, ItemID.ChlorophyteOre, ItemID.LunarOre };
            } else {
                return;
            }

            if(WorldGen.genRand.Next(2) == 0) {
                if(Main.netMode == 0) { //  Singleplayer
                    int index = Item.NewItem(target.position, oreSet[WorldGen.genRand.Next(oreSet.Length)], WorldGen.genRand.Next(10, 30));
                }else if(Main.netMode == 1) { //  MP
                    int index = Item.NewItem(target.position, oreSet[WorldGen.genRand.Next(oreSet.Length)], WorldGen.genRand.Next(10, 30));
                    NetMessage.SendData(21, -1, -1, Terraria.Localization.NetworkText.Empty, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                }
                target.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apAlreadyDroppedOre = true;
            }

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