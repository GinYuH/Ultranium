using Terraria;
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
		((Entity)(object)Item).width = 34;
		((Entity)(object)Item).height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 7;
		Item.defense = 12;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 10f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "AbyssRockItem", 45);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 35);
		val.AddTile(134);
		val.Register();
	}
}
