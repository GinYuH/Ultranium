using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class ShrineChestItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Guardian Chest");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 26;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.value = 500;
		((ModItem)this).Item.createTile = ModContent.TileType<ShrineChest>();
	}
}
