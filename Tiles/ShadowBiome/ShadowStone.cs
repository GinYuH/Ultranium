using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowStone : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Shadow Stone");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 12;
		((Entity)(object)((ModItem)this).Item).height = 12;
		((ModItem)this).Item.useTime = 8;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.value = 50;
		((ModItem)this).Item.rare = 0;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("ShadowStoneTile").Type;
		((ModItem)this).Item.maxStack = 999;
	}
}
