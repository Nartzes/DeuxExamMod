using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace DeuxExamMod.Items.Misc
{
    public class StudyGuide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Study Guide");
            Tooltip.SetDefault("Be prepare, but don't let them know.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(copper: 20);
            Item.rare = ItemRarityID.Green;
        }
    }
}
