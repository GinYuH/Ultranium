using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class DreadBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("DreadBossP2");

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
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadSword"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadBow"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadStaff"), 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadSummon"), 1);
		}
		if (Main.rand.Next(3) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadTooth"), 1);
		}
		if (Main.rand.Next(15) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadBreadItem"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadFlame"), Main.rand.Next(12, 32));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadScale"), Main.rand.Next(10, 16));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("DreadHeart"), 1);
	}
}
