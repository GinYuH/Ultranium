using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Trees;

public class ShadowTree : ModTree
{
	private Mod mod => ModLoader.GetMod("Ultranium");

	public override int DropWood()
	{
		return mod.ItemType("ShadowWood");
	}

	public override Texture2D GetTexture()
	{
		return mod.GetTexture("Tiles/ShadowBiome/Trees/ShadowTree");
	}

	public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
	{
		return mod.GetTexture("Tiles/ShadowBiome/Trees/ShadowTree_Tops");
	}

	public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
	{
		return mod.GetTexture("Tiles/ShadowBiome/Trees/ShadowTree_Branches");
	}
}
