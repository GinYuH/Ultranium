using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class IgnodiumShrineItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ignodium Shrine");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.rare = 0;
		((Entity)(object)((ModItem)this).Item).width = 12;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.value = 150;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("IgnodiumShrine").Type;
	}
}
