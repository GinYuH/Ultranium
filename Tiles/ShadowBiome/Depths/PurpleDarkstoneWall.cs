using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class PurpleDarkstoneWall : ModWall
{
	public override void SetDefaults()
	{
		Main.wallHouse[((ModWall)this).Type] = false;
		((ModWall)this).AddMapEntry(new Color(26, 0, 10), (LocalizedText)null);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
