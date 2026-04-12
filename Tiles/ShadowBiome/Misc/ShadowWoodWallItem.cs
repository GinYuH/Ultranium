using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Wood Wall");
		Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 11;
		Item.height = 11;
		Item.useTime = 6;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.value = 50;
		Item.rare = 0;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createWall = Mod.Find<ModWall>("ShadowWoodWall").Type;
		Item.maxStack = 999;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 4);
		val.AddIngredient((Mod)null, "ShadowWood", 1);
		val.AddTile(18);
		val.Register();
	}
}
