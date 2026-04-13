using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom.Armor;

[AutoloadEquip(EquipType.Legs)]
public class MushroomLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glowing Mushroom Legs");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = ItemRarityID.Blue;
		Item.defense = 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.GlowingMushroom, 14);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
