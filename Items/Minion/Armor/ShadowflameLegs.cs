using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(EquipType.Legs)]
public class ShadowflameLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Shadowflame Pants");
		Tooltip.SetDefault("3% increased summon damage and +1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 5;
		Item.defense = 12;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.03f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowFlame", 8);
		val.AddIngredient(521, 8);
		val.AddIngredient(225, 12);
		val.AddTile(134);
		val.Register();
	}
}
