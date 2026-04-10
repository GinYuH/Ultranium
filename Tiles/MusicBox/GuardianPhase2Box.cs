using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.MusicBox;

public class GuardianPhase2Box : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Music Box (Guardians Phase 2)");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("GuardianPhase2BoxTile").Type;
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.accessory = true;
	}
}
