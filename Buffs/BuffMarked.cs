using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;
using Terraria.ID;

namespace AmmoboxPlus.Buffs
{
    public class Marked : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Marked for Death");
            // Description.SetDefault("You are marked for death!"); 
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //ErrorLogger.Log("Enemy id " + npc.type + " update tick for Marked");
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apMarked = true;
            npc.netUpdate = true;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                npc.AddBuff(ModContent.BuffType<Marked>(), 100);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
            {  //  Does it actually work this way?
                NetMessage.SendData(0x35, -1, -1, Terraria.Localization.NetworkText.Empty, npc.whoAmI, ModContent.BuffType<Buffs.Marked>(), 100);
            }
        }
    }
}