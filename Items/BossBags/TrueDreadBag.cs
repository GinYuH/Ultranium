using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class TrueDreadBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("TrueDread");

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
		int num = Main.rand.Next(7);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadSpear"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadYoyo"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadDisc"), 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadFlameBlaster"), 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("FearStaff"), 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadTome"), 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadScepter"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("NightmareFuel"), Main.rand.Next(30, 45));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("TrueDreadHeart"), 1);
	}
}
