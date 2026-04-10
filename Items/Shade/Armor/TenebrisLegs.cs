using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class TenebrisLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Tenebris Greaves");
		// ((ModItem)this).Tooltip.SetDefault("2% increased damage\n5% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 22;
		((Entity)(object)((ModItem)this).Item).height = 16;
		((ModItem)this).Item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.defense = 4;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 7);
		val.AddTile(16);
		val.Register();
	}
}
