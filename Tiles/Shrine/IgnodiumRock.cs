using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Shrine;

public class IgnodiumRock : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(47, 62, 72), (LocalizedText)null);
		base.dustType = 6;
		base.drop = ((ModTile)this).mod.ItemType("IgnodiumRockItem");
		base.soundType = 21;
		base.soundStyle = 1;
		base.minPick = 1;
	}
}
