using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowOreTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		TileID.Sets.Ore[((ModTile)this).Type] = true;
		Main.tileSpelunker[((ModTile)this).Type] = true;
		Main.tileValue[((ModTile)this).Type] = 410;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Tenebris Ore");
		((ModTile)this).AddMapEntry(new Color(77, 2, 112), val);
		base.dustType = 65;
		base.drop = ((ModTile)this).mod.ItemType("ShadowOre");
		base.soundType = 21;
		base.soundStyle = 1;
		base.mineResist = 1.5f;
		base.minPick = 65;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.2f;
		g = 0.02f;
		b = 0.2f;
	}
}
