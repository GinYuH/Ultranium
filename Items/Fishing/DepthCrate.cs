using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class DepthCrate : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Depths Crate");
		((ModItem)this).Tooltip.SetDefault("Right click to open");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("DepthCrateTile");
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.consumable = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void RightClick(Player player)
	{
		int[] array = new int[3]
		{
			((ModItem)this).mod.ItemType("DepthGlowstoneItem"),
			((ModItem)this).mod.ItemType("NightmareBar"),
			((ModItem)this).mod.ItemType("ShadowEssence")
		};
		int num = Main.rand.Next(array.Length);
		player.QuickSpawnItem(array[num], Main.rand.Next(3, 5));
		int num2 = Main.rand.Next(3);
		if (num2 == 0)
		{
			player.QuickSpawnItem(2674, Main.rand.Next(3, 5));
		}
		if (num2 == 1)
		{
			player.QuickSpawnItem(2675, Main.rand.Next(2, 4));
		}
		if (num2 == 2)
		{
			player.QuickSpawnItem(2676, Main.rand.Next(1, 2));
		}
		int num3 = Main.rand.Next(2);
		if (num3 == 0)
		{
			player.QuickSpawnItem(499, Main.rand.Next(3, 5));
		}
		if (num3 == 1)
		{
			player.QuickSpawnItem(500, Main.rand.Next(3, 5));
		}
	}
}
