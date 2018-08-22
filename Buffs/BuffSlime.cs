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
            //  Fix funky Queen bee behaviour
            if(npc.type == NPCID.QueenBee) {
                npc.velocity *= 0.90f;
            } else {
                npc.velocity.X *= 0.90f;
            }
        }
    }
}