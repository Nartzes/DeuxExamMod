using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DeuxExamMod.Items.Consumeable;
using DeuxExamMod.Items.Misc;
using DeuxExamMod.Items.Accessories;
using DeuxExamMod.Items.Weapon.Melee;  // Namespace for FratPad
using DeuxExamMod.Items.Weapon.Range;  // Namespace for TShirtCannon
using DeuxExamMod.Items.Weapon.Magic;  // Correct namespace for DaPong

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
			NPC.CloneDefaults(NPCID.Skeleton);
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

		public override void OnKill()
		{
			// Manually spawn FeelGoodJuice
			SpawnItem<FeelGoodJuice>(1);

			// Manually spawn Pillow
			SpawnItem<Pillow>(1);

			// Manually spawn FratPad
			SpawnItem<FratPad>(1);

			// Manually spawn TShirtCannon
			SpawnItem<TShirtCannon>(1);

			// Manually spawn DaPong
			SpawnItem<DaPong>(1);

			// Manually spawn 3 gold coins
			Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.GoldCoin, 3);

			// Manually spawn a health potion
			Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.HealingPotion, 1);
		}

		// Helper method to spawn items
		private void SpawnItem<T>(int amount) where T : ModItem
		{
			int itemType = ModContent.ItemType<T>();
			if (itemType > 0 && amount > 0)
			{
				Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType, amount);
			}
		}
	}
}