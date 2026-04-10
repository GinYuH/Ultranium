using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class SquidBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("ZephyrSquid");

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
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("ZephyrBlade"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("ZephyrTrident"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("ZephyrKnife"), 1);
		}
		if (Main.rand.Next(20) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("WormPet"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("OceanScale"), Main.rand.Next(12, 16));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("MysticTentacle"), 1);
	}
}
