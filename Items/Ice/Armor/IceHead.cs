using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(EquipType.Head)]
public class IceHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ice Walker's Helmet");
		//Tooltip.SetDefault("3% increased damage");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = ItemRarityID.Orange;
		Item.defense = 5;
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
		if (body.type == Mod.Find<ModItem>("IceBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("IceLegs").Type;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.IceBlock, 35);
		val.AddIngredient((Mod)null, "IcePelt", 10);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
