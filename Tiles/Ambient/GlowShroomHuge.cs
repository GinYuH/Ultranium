using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomHuge : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileSolidTop[((ModTile)this).Type] = false;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileTable[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		((ModTile)this).AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		base.DustType = ((ModTile)this).Mod.Find<ModDust>("ShadowDustPurple").Type;
		((ModTile)this).AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.05f;
		g = 0.4f;
		b = 1.05f;
	}
}
