using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.MusicBox;

public class GuardianPhase1Box : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Music Box (Guardians Phase 1)");
		// Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.useStyle = 1;
		Item.useTurn = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("GuardianPhase1BoxTile").Type;
		Item.width = 24;
		Item.height = 24;
		Item.rare = 4;
		Item.accessory = true;
	}
}
