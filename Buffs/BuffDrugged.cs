using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Drugged : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Drugged");
            Description.SetDefault("Damage enemies nearby");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            //npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDruggedDamage = npc.damage;
            npc.GetGlobalNPC<AmmoboxGlobalNPC>(mod).apDrugged = true;
        }
    }
}