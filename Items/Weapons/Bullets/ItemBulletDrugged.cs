using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class BulletDrugged : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drugged Bullet");
            // Tooltip.SetDefault("Creates an aura around an enemy that damages nearby enemies");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 50;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("BulletDrugged").Type;
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.JungleSpores, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
