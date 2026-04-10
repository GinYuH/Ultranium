using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class AbyssRockItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Abyss Chunk");
		// ((ModItem)this).Tooltip.SetDefault("It feels rather gooey");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 12;
		((Entity)(object)((ModItem)this).Item).height = 12;
		((ModItem)this).Item.useTime = 8;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.value = 50;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("AbyssRock").Type;
		((ModItem)this).Item.maxStack = 999;
	}
}
