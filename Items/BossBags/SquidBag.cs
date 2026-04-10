using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class SquidBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("ZephyrSquid").Type;

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
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ZephyrBlade").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ZephyrTrident").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ZephyrKnife").Type, 1);
		}
		if (Main.rand.Next(20) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("WormPet").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("OceanScale").Type, Main.rand.Next(12, 16));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("MysticTentacle").Type, 1);
	}
}
