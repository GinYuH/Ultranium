using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles;

public class AuroraOre : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[Type][161] = true;
		Main.tileMerge[Type][147] = true;
		TileID.Sets.Ore[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 410;
		Main.tileSolid[Type] = true;
		AddMapEntry(new Color(240, 15, 207), (LocalizedText)null);
		base.DustType = DustID.GemAmethyst;
		base.MineResist = 2.5f;
		base.MinPick = 45;
	}
}
