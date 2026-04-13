using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood.Armor;

[AutoloadEquip(EquipType.Body)]
public class BloodBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Sanguine Chestplate");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = ItemRarityID.Green;
		Item.defense = 5;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "BloodClot", 20);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 15);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
