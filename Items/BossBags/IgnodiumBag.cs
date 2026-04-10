using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class IgnodiumBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("Ignodium").Type;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		// ((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.consumable = true;
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
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
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellFlail").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellThrow").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellGun").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellJavelin").Type, 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellStaff").Type, 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellTome").Type, 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellScepter").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("HellShard").Type, Main.rand.Next(30, 40));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("IgnodiumRelic").Type, 1);
	}
}
