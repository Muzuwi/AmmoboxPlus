using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Cold : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Cold");
            Description.SetDefault("You can barely move");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apCold = true;
            if(npc.boss){
                npc.velocity *= 0.95f;
            } else {
                npc.velocity *= 0.7f;
            }
        }
    }
}