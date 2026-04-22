using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Ultranium.Dusts;

namespace Ultranium.Tiles.Ambient.Purple;

public class PurpleGlowShroomHuge : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.addTile((int)Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        DustType = ModContent.DustType<ShadowDustPurple>();
        AddMapEntry(new Color(52, 6, 40), (LocalizedText)null);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.05f;
		g = 0.4f;
		b = 1.05f;
	}
}
