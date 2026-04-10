using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomHuge2 : ModTile
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
		base.DustType = Mod.Find<ModDust>("ShadowDustPurple").Type;
		AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.05f;
		g = 0.4f;
		b = 1.05f;
	}
}
