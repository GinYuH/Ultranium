using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodWall : ModWall
{
	public override void SetDefaults()
	{
		Main.wallHouse[((ModWall)this).Type] = true;
		base.drop = ((ModWall)this).mod.ItemType("ShadowWoodWallItem");
		((ModWall)this).AddMapEntry(new Color(8, 6, 15), (LocalizedText)null);
	}
}
