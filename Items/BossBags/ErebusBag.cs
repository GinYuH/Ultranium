using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class ErebusBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).mod.NPCType("ErebusHead");

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.consumable = true;
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
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
		int num = Main.rand.Next(9);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Noctis"), 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("SolibusOrba"), 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Exitium"), 1);
		}
		if (num == 3)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Crepus"), 1);
		}
		if (num == 4)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Inanis"), 1);
		}
		if (num == 5)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("CavumNigrum"), 1);
		}
		if (num == 6)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Umbra"), 1);
		}
		if (num == 7)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Nihil"), 1);
		}
		if (num == 8)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("Caliginus"), 1);
		}
		if (Main.rand.Next(20) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).mod.ItemType("ErebusGuitar"), 1);
		}
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("NightmareScale"), Main.rand.Next(30, 50));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("DarkMatter"), Main.rand.Next(20, 30));
		player.QuickSpawnItem(((ModItem)this).mod.ItemType("ShadowHeart"), 1);
	}
}
