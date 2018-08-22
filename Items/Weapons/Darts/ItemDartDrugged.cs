using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartDrugged : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Drugged Dart");
            Tooltip.SetDefault("Creates an aura around an enemy that damages nearby enemies");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartDrugged");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 10);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
