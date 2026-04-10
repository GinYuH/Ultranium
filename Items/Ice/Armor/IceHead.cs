using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class IceHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ice Walker's Helmet");
		((ModItem)this).Tooltip.SetDefault("3% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.defense = 5;
	}

	public override void UpdateEquip(Player player)
	{
		player.meleeDamage += 0.03f;
		player.rangedDamage += 0.03f;
		player.minionDamage += 0.03f;
		player.magicDamage += 0.03f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("IceBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("IceLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nImmunity to all ice related debuffs\n3% increased damage, melee speed, and movement speed";
		player.buffImmune[44] = true;
		player.buffImmune[46] = true;
		player.buffImmune[47] = true;
		player.meleeDamage += 0.03f;
		player.rangedDamage += 0.03f;
		player.minionDamage += 0.03f;
		player.magicDamage += 0.03f;
		player.meleeSpeed += 0.03f;
		player.moveSpeed += 3f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(664, 35);
		val.AddIngredient((Mod)null, "IcePelt", 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
