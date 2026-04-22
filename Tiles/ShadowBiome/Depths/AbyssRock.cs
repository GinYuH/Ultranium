using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Dusts;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class AbyssRock : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		AddMapEntry(new Color(51, 49, 95), (LocalizedText)null);
        DustType = ModContent.DustType<ShadowDustPurple>();
        //base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("AbyssRockItem").Type;
        HitSound = SoundID.Tink;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		MineResist = 5f;
		MinPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
