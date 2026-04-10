using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MushroomBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Glowing Mushroom Chestplate");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = 1;
		Item.defense = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(183, 16);
		val.AddTile(16);
		val.Register();
	}
}
