using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class IceHead : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ice Walker's Helmet");
		// ((ModItem)this).Tooltip.SetDefault("3% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.defense = 5;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.03f;
		player.GetDamage(DamageClass.Ranged) += 0.03f;
		player.GetDamage(DamageClass.Summon) += 0.03f;
		player.GetDamage(DamageClass.Magic) += 0.03f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("IceBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("IceLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nImmunity to all ice related debuffs\n3% increased damage, melee speed, and movement speed";
		player.buffImmune[44] = true;
		player.buffImmune[46] = true;
		player.buffImmune[47] = true;
		player.GetDamage(DamageClass.Melee) += 0.03f;
		player.GetDamage(DamageClass.Ranged) += 0.03f;
		player.GetDamage(DamageClass.Summon) += 0.03f;
		player.GetDamage(DamageClass.Magic) += 0.03f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
		player.moveSpeed += 3f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(664, 35);
		val.AddIngredient((Mod)null, "IcePelt", 10);
		val.AddTile(16);
		val.Register();
	}
}
