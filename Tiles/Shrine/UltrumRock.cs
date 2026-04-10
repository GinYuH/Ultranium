using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Shrine;

public class UltrumRock : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(49, 37, 34), (LocalizedText)null);
		base.dustType = ((ModTile)this).mod.DustType("UltraniumDust");
		base.drop = ((ModTile)this).mod.ItemType("UltrumRockItem");
		base.soundType = 21;
		base.soundStyle = 1;
		base.minPick = 1;
	}
}
