using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class DepthCrate : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Depths Crate");
		// Tooltip.SetDefault("Right click to open");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.rare = 7;
		Item.useStyle = 1;
		Item.createTile = Mod.Find<ModTile>("DepthCrateTile").Type;
		Item.maxStack = 999;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.consumable = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void RightClick(Player player)
	{
		int[] array = new int[3]
		{
			Mod.Find<ModItem>("DepthGlowstoneItem").Type,
			Mod.Find<ModItem>("NightmareBar").Type,
			Mod.Find<ModItem>("ShadowEssence").Type
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
