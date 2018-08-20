using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartMarked : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Marker Dart");
            Tooltip.SetDefault("Inflicts 'Marked for Death'.\nInflicted enemies receive 15% more damage.");
        }

        public override void SetDefaults() {
            item.damage = 3;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1f; 
            item.value = 250; 
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("DartMarked");
            item.shootSpeed = 10f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);  //  TODO: CHANGE
            recipe.AddIngredient(ItemID.EmptyBullet, 1);    //  TODO: CHANGE
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

}
