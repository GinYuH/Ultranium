using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class EtherealBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("Xenanis");

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.consumable = true;
		((Entity)(object)((ModItem)this).item).width = 36;
		((Entity)(object)((ModItem)this).item).height = 34;
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
		int num = Main.rand.Next(4);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealSword"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealBow"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealTome"), 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealSummon"), 1);
		}
		if (Main.rand.Next(15) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("XenanisWings"), 1);
		}
		if (Main.rand.Next(8) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealDidgeridoo"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("XenanisFlesh"), Main.rand.Next(15, 25));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("EtherealCore"), 1);
	}
}
