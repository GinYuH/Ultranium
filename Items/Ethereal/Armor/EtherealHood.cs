using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal.Armor;

[AutoloadEquip(EquipType.Head)]
public class EtherealHood : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Occultist Hood");
		Tooltip.SetDefault("8% increased damage\n+5 max life and mana\n+1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = 9;
		Item.defense = 11;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("EtherealBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("EtherealLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nGrants increased immune time after being hit\nGrants the ability to dash\nEnemies are much less likely to target you";
		player.longInvince = true;
		player.dash = 1;
		player.aggro -= 400;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions++;
		player.GetDamage(DamageClass.Melee) += 0.08f;
		player.GetDamage(DamageClass.Ranged) += 0.08f;
		player.GetDamage(DamageClass.Magic) += 0.08f;
		player.GetDamage(DamageClass.Summon) += 0.08f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 8);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddIngredient(225, 10);
		val.AddTile(134);
		val.Register();
	}
}
