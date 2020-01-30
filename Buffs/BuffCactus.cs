using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs {
    public class Cactus : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Spiky Shield");
            Description.SetDefault("Enemies nearby are hurt");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = true;
            //npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactusDamage = npc.damage;
        }
    }
}