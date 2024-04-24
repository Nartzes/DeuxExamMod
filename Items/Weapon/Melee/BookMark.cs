using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace DeuxExamMod.Items.Weapon.Melee
{
    public class BookMark : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 75;
            Item.crit = 1;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 45;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.rare = 6;
            Item.shoot = Mod.Find<ModProjectile>("BookProj").Type;
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

    }

}