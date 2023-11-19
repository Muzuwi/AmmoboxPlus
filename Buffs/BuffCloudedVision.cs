using Terraria;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs
{
    public class CloudedVision : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Clouded Vision");
            // Description.SetDefault("Your hit accuracy is decreased");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apClouded = true;
        }
    }
}