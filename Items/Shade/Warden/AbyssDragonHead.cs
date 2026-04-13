using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(EquipType.Head)]
public class AbyssDragonHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Dragon Mask");
		//Tooltip.SetDefault("10% increased melee speed and damage\n12% increased melee critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 34;
		((Entity)(object)Item).height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 7;
		Item.defense = 23;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.1f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
		player.GetCritChance(DamageClass.Melee) += 12;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("AbyssWardenBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("AbyssWardenLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n20% increased melee and movement speed";
		player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		player.moveSpeed += 0.2f;
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
