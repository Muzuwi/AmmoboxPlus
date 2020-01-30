using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class CloudedVision : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Clouded Vision");
            Description.SetDefault("Your hit accuracy is decreased");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apClouded = true;
        }
    }
}