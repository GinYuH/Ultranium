using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class AldinBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("Aldin");

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
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("CosmicBlade"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("CosmicBow"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("CosmicStaff"), 1);
		}
		int num2 = Main.rand.Next(3);
		if (num2 == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("AldinHood"), 1);
		}
		if (num2 == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("AldinBody"), 1);
		}
		if (num2 == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("AldinRobe"), 1);
		}
		if (Main.rand.Next(10) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("CosmicIdol"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("CosmicWings"), 1);
		if (Main.rand.Next(20) == 0)
		{
			int num3 = Main.rand.Next(4);
			if (num3 == 0)
			{
				player.QuickSpawnItem(((ModItem)this).mod.ItemType("RayGun"), 1);
			}
			if (num3 == 1)
			{
				player.QuickSpawnItem(((ModItem)this).mod.ItemType("DevotedKatana"), 1);
			}
			if (num3 == 3)
			{
				player.QuickSpawnItem(((ModItem)this).mod.ItemType("ShadowFlute"), 1);
			}
			if (num3 == 4)
			{
				player.QuickSpawnItem(((ModItem)this).mod.ItemType("DemonicSingularity"), 1);
			}
			if (num3 == 5)
			{
				player.QuickSpawnItem(((ModItem)this).mod.ItemType("Necromicon"), 1);
			}
		}
	}
}
