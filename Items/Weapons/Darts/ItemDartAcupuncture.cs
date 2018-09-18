using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartAcupuncture : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Acupuncture Dart");
            Tooltip.SetDefault("Gets stronger with each hit.\nThe bonus is reset when you miss the enemy.");
        }

        public override void SetDefaults() {
            item.damage = 1;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 15; 
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("DartAcupuncture");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }

    }

}
