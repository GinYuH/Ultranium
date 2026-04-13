using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowStoneTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[Type][ModContent.TileType<ShadowOreTile>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		AddMapEntry(new Color(37, 41, 58), (LocalizedText)null);
		base.DustType = Mod.Find<ModDust>("ShadowStoneDust").Type;
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("ShadowStone").Type;
		base.HitSound = SoundID.Tink;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MineResist = 1f;
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Framing.GetTileSafely(i, j - 1).TileType == TileID.Dirt && Framing.GetTileSafely(i, j - 2).TileType == TileID.Dirt && Main.rand.Next(10) == 0 && Main.rand.Next(10) == 0)
		{
			WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroom>());
			NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroom>(), 0, 0, -1, -1);
		}
	}
}
