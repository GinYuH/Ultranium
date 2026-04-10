using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class DepthCrate : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Depths Crate");
		// ((ModItem)this).Tooltip.SetDefault("Right click to open");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("DepthCrateTile").Type;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.consumable = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void RightClick(Player player)
	{
		int[] array = new int[3]
		{
			((ModItem)this).Mod.Find<ModItem>("DepthGlowstoneItem").Type,
			((ModItem)this).Mod.Find<ModItem>("NightmareBar").Type,
			((ModItem)this).Mod.Find<ModItem>("ShadowEssence").Type
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
