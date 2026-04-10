using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowWall : ModWall
{
	public override void SetStaticDefaults()
	{
		Main.wallHouse[((ModWall)this).Type] = false;
		((ModWall)this).AddMapEntry(new Color(52, 0, 40), (LocalizedText)null);
	}
}
