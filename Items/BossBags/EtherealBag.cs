using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class EtherealBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("Xenanis").Type;

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
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealSword").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealBow").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealTome").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealSummon").Type, 1);
		}
		if (Main.rand.Next(15) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("XenanisWings").Type, 1);
		}
		if (Main.rand.Next(8) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealDidgeridoo").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("XenanisFlesh").Type, Main.rand.Next(15, 25));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("EtherealCore").Type, 1);
	}
}
