using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamModv1.Items
{
    public class BookMark : ModItem //Can't get this to inherit/
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Throwing;
            Item.crit = 16;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation =10 ;
            Item.useStyle = 5;
            Item.knockBack = 8;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.Ironskin, 7200);
            player.AddBuff(BuffID.Wrath, 7200);
            player.AddBuff(BuffID.Slow, 2000);
            return true;


        }
    }
}