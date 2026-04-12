using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWood : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Wood");
		Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.useTime = 8;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.value = 50;
		Item.rare = 0;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("ShadowWoodTile").Type;
		Item.maxStack = 999;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowWoodWallItem", 4);
		val.AddTile(18);
		val.Register();
	}
}
