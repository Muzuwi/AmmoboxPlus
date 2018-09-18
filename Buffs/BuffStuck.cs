using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Stuck : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Stuck");
            Description.SetDefault("You are stuck in place!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apStuck = true;
        }
    }
}