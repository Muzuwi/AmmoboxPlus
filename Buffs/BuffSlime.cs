using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Slime : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Slime");
            Description.SetDefault("Your movements are limited.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apSlime = true;
            if(npc.boss) {
                npc.velocity *= 0.98f;
            } else {
                npc.velocity *= 0.90f;
            }
        }
    }
}