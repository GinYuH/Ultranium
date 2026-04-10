using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Trees;

public class ShadowTree : ModTree
{
	private Mod mod => ModLoader.GetMod("Ultranium");

	public override int DropWood()
	{
		return mod.Find<ModItem>("ShadowWood").Type;
	}

    public override int SaplingGrowthType(ref int style)
    {
        return ModContent.TileType<ShadowTreeSapling>();
    }


    public override Asset<Texture2D> GetBranchTextures()
    {
        return ModContent.Request<Texture2D>("Ultranium/Tiles/ShadowBiome/Trees/ShadowTree_Branches");
    }

    public override Asset<Texture2D> GetTopTextures()
    {
        return ModContent.Request<Texture2D>("Ultranium/Tiles/ShadowBiome/Trees/ShadowTree_Tops");
    }

    public override void SetStaticDefaults()
    {
        GrowsOnTileId = new int[] { ModContent.TileType<ShadowGrass>(), ModContent.TileType<PurpleShadowGrass>() };
    }

    public override Asset<Texture2D> GetTexture()
    {
        return ModContent.Request<Texture2D>("Ultranium/Tiles/ShadowBiome/Trees/ShadowTree");
    }

    public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
    {
        UseSpecialGroups = true,
        SpecialGroupMinimalHueValue = 11f / 72f,
        SpecialGroupMaximumHueValue = 0.25f,
        SpecialGroupMinimumSaturationValue = 0.88f,
        SpecialGroupMaximumSaturationValue = 1f
    };
}
