using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(EquipType.Head)]
public class StellarHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Stone Mask");
		//Tooltip.SetDefault("5% increased damage\n+1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = ItemRarityID.Pink;
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "StellarBar", 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
