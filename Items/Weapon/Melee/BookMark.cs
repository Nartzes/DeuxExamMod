//using Terraria.DataStructures;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria;
//using Microsoft.Xna.Framework;

//namespace DeuxExamMod.Items.Weapon.Melee
//{
//    public class BookMark : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Book Mark");
//            Tooltip.SetDefault("Send the razor sharp edges into their eyes.");
//        }

//        public override void SetDefaults()
//        {
//            Item.DamageType = DamageClass.MeleeNoSpeed;
//            Item.damage = 250;
//            Item.crit = 1;
//            Item.width = 20;
//            Item.height = 20;
//            Item.useTime = 50;
//            Item.useStyle = ItemUseStyleID.Swing;
//            Item.useAnimation = 50;
//            Item.noUseGraphic = true;
//            Item.noMelee = true;
//            Item.knockBack = 3;
//            Item.rare = ItemRarityID.Purple;
//            Item.shoot = Mod.Find<ModProjectile>("BookProj").Type;
//            Item.shootSpeed = 12f;
//            Item.UseSound = SoundID.Item1;
//            Item.autoReuse = true;
//        }

//    }

//}




//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace DeuxExamMod.Items.Misc
//{
//    public class BookMark : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("BookMark");
//            Tooltip.SetDefault("A helpful bookmark.");
//        }

//        public override void SetDefaults()
//        {
//            Item.width = 20;
//            Item.height = 20;
//            Item.maxStack = 99;
//            Item.value = 100;
//            Item.rare = 1;
//        }

//        // Explicitly set the texture path
//        public override string Texture => "DeuxExamMod/Items/Misc/BookMark";
//    }
//}


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Weapon.Melee
{
    public class BookMark : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BookMark");
            Tooltip.SetDefault("A helpful bookmark.");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.value = 100;
            Item.rare = 1;
        }

        // Explicitly set the texture path
        public override string Texture => "DeuxExamMod/Items/Weapon/Melee/BookMark";
    }
}
