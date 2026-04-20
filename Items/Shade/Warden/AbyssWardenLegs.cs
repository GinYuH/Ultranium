using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(EquipType.Legs)]
public class AbyssWardenLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Greaves");
		//Tooltip.SetDefault("10% increased movement speed");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Lime;
		Item.defense = 12;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 0.1f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "AbyssRockItem", 45);
		val.AddIngredient(null, "DepthGlowstoneItem", 35);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
