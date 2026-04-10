using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.MusicBox;

public class FlayerMusicBox : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Music Box (Mindflayer)");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("FlayerMusicBoxTile");
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.accessory = true;
	}
}
