using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class StellarHead : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Stellar Stone Mask");
		Tooltip.SetDefault("5% increased damage\n+1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 18;
		((Entity)(object)Item).height = 18;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = 5;
		Item.defense = 14;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("StellarBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("StellarLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nYou have a chance to unleash a circle of comets when you are hit";
		player.GetModPlayer<UltraniumPlayer>().StellarSet = true;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions++;
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.GetDamage(DamageClass.Magic) += 0.05f;
		player.GetDamage(DamageClass.Summon) += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
