using IL.Terraria.ID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DeuxExamModv1.Items
{
    internal class TShirtCannon: ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo.CompareTo(10);
            Item.shootSpeed = 12
            Item.crit = 16;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 8;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            }
        }
}
