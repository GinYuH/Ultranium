using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class IceDragonBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("IceDragon");

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
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("GlacialFlail"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("GlacialGun"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("GlacialWand"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("IcePelt"), Main.rand.Next(15, 22));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("IceTalon"), 1);
	}
}
