using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class ErebusBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("ErebusHead").Type;

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
		int num = Main.rand.Next(9);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Noctis").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("SolibusOrba").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Exitium").Type, 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Crepus").Type, 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Inanis").Type, 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CavumNigrum").Type, 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Umbra").Type, 1);
		}
		if (num == 7)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Nihil").Type, 1);
		}
		if (num == 8)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Caliginus").Type, 1);
		}
		if (Main.rand.Next(20) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ErebusGuitar").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("NightmareScale").Type, Main.rand.Next(30, 50));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(20, 30));
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ShadowHeart").Type, 1);
	}
}
