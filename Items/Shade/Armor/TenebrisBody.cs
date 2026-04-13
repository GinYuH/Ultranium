using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(EquipType.Body)]
public class TenebrisBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Chestmail");
		//Tooltip.SetDefault("3% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 34;
		((Entity)(object)Item).height = 22;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.03f;
		player.GetDamage(DamageClass.Ranged) += 0.03f;
		player.GetDamage(DamageClass.Summon) += 0.03f;
		player.GetDamage(DamageClass.Magic) += 0.03f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.Register();
	}
}
