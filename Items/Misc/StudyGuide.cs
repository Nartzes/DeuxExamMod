using DeuxExamMod.Items.Misc;
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
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipeModded = CreateRecipe();
            recipeModded.AddIngredient<CollegeNote>(8);
            recipeModded.AddTile(18);
            recipeModded.Register();
        }


    }
}
