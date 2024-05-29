//using Terraria;999999
//using Terraria.DataStructures;
//using Terraria.ModLoader;
//using Terraria.ID;

//namespace DeuxExamMod.Items.Misc
//{
//    public class CollegeNote : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("College Notes");
//            Tooltip.SetDefault("On that gpa grind.");
//        }

//        public override void SetDefaults()
//        {
//            Item.width = 16;
//            Item.height = 28;
//            Item.maxStack = 999;
//            Item.rare = ItemRarityID.Blue;
//        }
//    }
//}


using Terraria.ModLoader;

namespace DeuxExamMod.Items.Misc
{
    public class CollegeNote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("College Note");
            Tooltip.SetDefault("A note filled with college wisdom.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
