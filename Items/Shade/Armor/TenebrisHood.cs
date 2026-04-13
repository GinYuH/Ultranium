using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(EquipType.Head)]
public class TenebrisHood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Hood");
		//Tooltip.SetDefault("2% increased magic critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 24;
		((Entity)(object)Item).height = 20;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("TenebrisBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("TenebrisLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "6% increased magic damage";
		player.GetDamage(DamageClass.Magic) += 0.06f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Magic) += 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 8);
		val.AddTile(16);
		val.Register();
	}
}
