using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Marked : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Marked for Death");
            Description.SetDefault("You are marked for death!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            //ErrorLogger.Log("Enemy id " + npc.type + " update tick for Marked");
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apMarked = true;
            npc.netUpdate = true;
            if(Main.netMode == 0) {
                npc.AddBuff(ModContent.BuffType<Buffs.Marked>(), 100);
            }else if(Main.netMode == 1 || Main.netMode == 2) {  //  Does it actually work this way?
                NetMessage.SendData(0x35, -1, -1, Terraria.Localization.NetworkText.Empty, npc.whoAmI, ModContent.BuffType<Buffs.Marked>(), 100);
            }
        }
    }
}