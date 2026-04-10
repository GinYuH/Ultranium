using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class DreadBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("DreadBossP2").Type;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		// ((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.consumable = true;
		((Entity)(object)((ModItem)this).Item).width = 36;
		((Entity)(object)((ModItem)this).Item).height = 34;
		((ModItem)this).Item.rare = -12;
		((ModItem)this).Item.expert = true;
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
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadSword").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadBow").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadStaff").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadSummon").Type, 1);
		}
		if (Main.rand.Next(3) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadTooth").Type, 1);
		}
		if (Main.rand.Next(15) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadBreadItem").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadFlame").Type, Main.rand.Next(12, 32));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadScale").Type, Main.rand.Next(10, 16));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadHeart").Type, 1);
	}
}
