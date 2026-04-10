using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.ShadowEvent;

public class FlayerWraithBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Flayer Wraith Banner");
		((ModItem)this).Tooltip.SetDefault("Nearby players get a bonus against: Flayer Wraith");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 10;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 10);
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("ShadowEventBanners");
		((ModItem)this).item.placeStyle = 6;
	}
}
