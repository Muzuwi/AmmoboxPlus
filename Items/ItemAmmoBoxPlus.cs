using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AmmoboxPlus.Items
{
    class AmmoBoxPlus : ModItem
    {
        private static readonly int DEV_WEAPON_CHANCE_1_IN_X = 20;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ammo Box Plus");
            // Tooltip.SetDefault("A box containing some ammo\nIt's very heavy and radiating an aura of Power and Conflict\nRight Click to open");
            Item.value = 100;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 30;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            //  Spawn a dev weapon
            if (Main.rand.NextBool(DEV_WEAPON_CHANCE_1_IN_X))
            {
                List<int> weaponList = CreateDevWeaponIdList();
                if (weaponList.Count > 0)
                {
                    int idx = Main.rand.Next(0, weaponList.Count);
                    int rolledWeaponId = weaponList[idx];
                    player.QuickSpawnItemDirect(player.GetSource_GiftOrReward(), rolledWeaponId, 1);
                }
            }

            //  Load drop table
            var dropTable = new WeightedRandom<int>();
            foreach (var a in AmmoboxPlus.AmmoboxVanillaHMAmmo)
            {
                dropTable.Add(a.Key);
            }

            if (AmmoboxPlus.AmmoboxModAmmoHM.Count > 0)
            {
                foreach (var a in AmmoboxPlus.AmmoboxModAmmoHM)
                {
                    dropTable.Add(a.Key);
                }
            }

            int randomChoice = dropTable;
            int amount = 0;
            if (AmmoboxPlus.AmmoboxVanillaHMAmmo.ContainsKey(randomChoice))
            {
                amount = AmmoboxPlus.AmmoboxVanillaHMAmmo[randomChoice];
            }
            else if (AmmoboxPlus.AmmoboxModAmmoHM.ContainsKey(randomChoice))
            {
                amount = AmmoboxPlus.AmmoboxModAmmoHM[randomChoice];
            }
            var _ = player.QuickSpawnItemDirect(player.GetSource_GiftOrReward(), randomChoice, amount);
        }

        private List<int> CreateDevWeaponIdList()
        {
            List<int> weaponList = new List<int>();

            if (NPC.downedPlantBoss)
            {
                weaponList.Add(Mod.Find<ModItem>("Marine").Type);
                weaponList.Add(Mod.Find<ModItem>("Boombox").Type);
            }
            if (NPC.downedMoonlord)
            {
                weaponList.Add(Mod.Find<ModItem>("BoneGun").Type);
            }
            if (Main.hardMode)
            {
                weaponList.Add(Mod.Find<ModItem>("DartScrapper").Type);
            }
            return weaponList;
        }
    }
}