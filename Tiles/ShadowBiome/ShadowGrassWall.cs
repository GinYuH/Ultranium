using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowGrassWall : ModWall
{
	public override void SetDefaults()
	{
		Main.wallHouse[((ModWall)this).Type] = false;
		((ModWall)this).AddMapEntry(new Color(37, 63, 40), (LocalizedText)null);
	}
}
