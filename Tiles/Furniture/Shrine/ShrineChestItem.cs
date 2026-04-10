using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class ShrineChestItem : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Guardian Chest");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 26;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.value = 500;
		((ModItem)this).item.createTile = ModContent.TileType<ShrineChest>();
	}
}
