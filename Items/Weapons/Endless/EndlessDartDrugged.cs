using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartDrugged : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Drugged Pouch");
            Tooltip.SetDefault("Creates an aura around an enemy that damages nearby enemies");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartDrugged");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Dart;
        }

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartDrugged"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}