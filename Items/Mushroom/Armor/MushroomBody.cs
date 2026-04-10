using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MushroomBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Chestplate");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.defense = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(183, 16);
		val.AddTile(16);
		val.Register();
	}
}
