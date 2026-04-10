using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class UltrumBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("Ultrum").Type;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		// ((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.consumable = true;
		((Entity)(object)((ModItem)this).Item).width = 46;
		((Entity)(object)((ModItem)this).Item).height = 36;
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
		int num = Main.rand.Next(7);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraFlail").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraniumBow").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraniumKunai").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraniumStaff").Type, 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraniumSword").Type, 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraTome").Type, 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltraniumScepter").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltrumShard").Type, Main.rand.Next(30, 40));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("UltrumRelic").Type, 1);
	}
}
