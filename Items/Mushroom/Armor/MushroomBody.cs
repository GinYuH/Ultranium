using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MushroomBody : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Chestplate");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(183, 16);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
