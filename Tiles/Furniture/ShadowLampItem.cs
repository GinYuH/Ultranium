using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowLampItem : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Lamp");
	}

	public override void SetDefaults()
	{
		Item.rare = 0;
		Item.width = 12;
		Item.height = 30;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = 1;
		Item.consumable = true;
		Item.value = 150;
		Item.createTile = Mod.Find<ModTile>("ShadowLamp").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowWood", 6);
		val.AddIngredient(8, 1);
		val.AddTile(18);
		val.Register();
	}
}
