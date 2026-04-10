using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class UltrumBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("Ultrum");

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.consumable = true;
		((Entity)(object)((ModItem)this).item).width = 46;
		((Entity)(object)((ModItem)this).item).height = 36;
		((ModItem)this).item.rare = -12;
		((ModItem)this).item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void OpenBossBag(Player player)
	{
		player.TryGettingDevArmor();
		int num = Main.rand.Next(7);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraFlail"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraniumBow"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraniumKunai"), 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraniumStaff"), 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraniumSword"), 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraTome"), 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltraniumScepter"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltrumShard"), Main.rand.Next(30, 40));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("UltrumRelic"), 1);
	}
}
