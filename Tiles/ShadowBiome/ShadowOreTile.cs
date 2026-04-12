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
		Main.tileMergeDirt[Type] = true;
		Main.tileMerge[Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowGrass>()] = true;
		TileID.Sets.Ore[Type] = true;
		Main.tileSpelunker[Type] = true;
		Main.tileOreFinderPriority[Type] = 410;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Tenebris Ore");
		AddMapEntry(new Color(77, 2, 112), val);
		base.DustType = 65;
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("ShadowOre").Type;
		base.HitSound = SoundID.Tink;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
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
