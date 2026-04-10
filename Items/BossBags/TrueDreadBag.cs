using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class TrueDreadBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("TrueDread").Type;

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
		int num = Main.rand.Next(7);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadSpear").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadYoyo").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadDisc").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadFlameBlaster").Type, 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("FearStaff").Type, 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadTome").Type, 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DreadScepter").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("NightmareFuel").Type, Main.rand.Next(30, 45));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("TrueDreadHeart").Type, 1);
	}
}
