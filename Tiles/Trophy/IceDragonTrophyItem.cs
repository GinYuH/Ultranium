using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Trophy;

public class IceDragonTrophyItem : ModItem
{
	public static int _type;

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacieron Trophy");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.value = 0;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("IceDragonTrophy");
		((ModItem)this).item.placeStyle = 0;
	}
}
