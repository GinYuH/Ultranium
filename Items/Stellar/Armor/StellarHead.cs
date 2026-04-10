using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class StellarHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Stellar Stone Mask");
		((ModItem)this).Tooltip.SetDefault("5% increased damage\n+1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.defense = 14;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("StellarBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("StellarLegs");
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
		player.meleeDamage += 0.05f;
		player.rangedDamage += 0.05f;
		player.magicDamage += 0.05f;
		player.minionDamage += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
