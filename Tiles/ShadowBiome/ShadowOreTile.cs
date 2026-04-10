using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowOreTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		TileID.Sets.Ore[((ModTile)this).Type] = true;
		Main.tileSpelunker[((ModTile)this).Type] = true;
		Main.tileOreFinderPriority[((ModTile)this).Type] = 410;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		// val.SetDefault("Tenebris Ore");
		((ModTile)this).AddMapEntry(new Color(77, 2, 112), val);
		base.DustType = 65;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ((ModTile)this).Mod.Find<ModItem>("ShadowOre").Type;
		base.HitSound = 21;
		base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MineResist = 1.5f;
		base.MinPick = 65;
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
