using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    class EndlessDartYang : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Yang Pouch");
            Tooltip.SetDefault("Deals great damage when paired with the Ying Dart.\n'All good contains a modicum of evil'");
        }

        public override void SetDefaults() {
            item.damage = 2;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 150; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartYang");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartYing"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player){
            return false;
        }
    }
}
