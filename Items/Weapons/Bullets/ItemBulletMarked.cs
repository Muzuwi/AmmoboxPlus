using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class BulletMarked : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Markershot");
            // Tooltip.SetDefault("Inflicts 'Marked for Death'.\nInflicted enemies receive 15% more damage.");
        }

        public override void SetDefaults()
        {
            Item.damage = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 250;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = Mod.Find<ModProjectile>("BulletMarked").Type;
            Item.shootSpeed = 10f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

}
