using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.MusicBox;

public class GuardianPhase1Box : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanGetPrefixes[Type] = false;
		ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
	}

	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTurn = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("GuardianPhase1BoxTile").Type;
		Item.width = 24;
		Item.height = 24;
		Item.rare = ItemRarityID.LightRed;
		Item.accessory = true;
	}
}
