using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class UltrumShrineItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ultrum Shrine");
	}

	public override void SetDefaults()
	{
		Item.rare = 0;
		Item.width = 12;
		Item.height = 30;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = 1;
		Item.consumable = true;
		Item.value = 150;
		Item.createTile = Mod.Find<ModTile>("UltrumShrine").Type;
	}
}
