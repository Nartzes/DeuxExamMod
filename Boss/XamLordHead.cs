using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Boss
{
    public class XamLordHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XamLord Head");
        }

        public override void SetDefaults()
        {
            // Clone the defaults from the Skeleton NPC
            NPC.CloneDefaults(NPCID.Skeleton);

            // Modify the cloned defaults as needed
            NPC.width = 20;
            NPC.height = 20;
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 100;
            NPC.value = 100f;
            NPC.knockBackResist = 0.4f;
        }

        public override void AI()
        {
            // Additional AI code if needed
        }
    }
}

