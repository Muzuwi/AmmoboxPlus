using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class BoneGun : ModItem
    {
        private static readonly int NO_AMMO_CONSUME_1_IN_X = 2;

        public override void SetDefaults()
        {
            // DisplayName.SetDefault("Kranot's Kaliberfracture");
            // Tooltip.SetDefault("An artist's weapon of choice\nThank you for supporting Ammobox+!");
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.crit += 10;
            Item.width = 66;
            Item.height = 32;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item40;
            Item.damage = 77;
            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.expert = true;
            Item.expertOnly = false;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Ranged;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextBool(NO_AMMO_CONSUME_1_IN_X);
        }
    }
}
