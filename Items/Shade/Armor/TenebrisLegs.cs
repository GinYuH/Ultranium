using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(EquipType.Legs)]
public class TenebrisLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Tenebris Greaves");
		Tooltip.SetDefault("2% increased damage\n5% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 22;
		((Entity)(object)Item).height = 16;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.02f;
		player.GetDamage(DamageClass.Ranged) += 0.02f;
		player.GetDamage(DamageClass.Summon) += 0.02f;
		player.GetDamage(DamageClass.Magic) += 0.02f;
		player.moveSpeed += 0.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 7);
		val.AddTile(16);
		val.Register();
	}
}
