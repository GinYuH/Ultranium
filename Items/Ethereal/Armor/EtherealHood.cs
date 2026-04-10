using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class EtherealHood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Occultist Hood");
		((ModItem)this).Tooltip.SetDefault("8% increased damage\n+5 max life and mana\n+1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.defense = 11;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("EtherealBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("EtherealLegs");
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
		player.meleeDamage += 0.08f;
		player.rangedDamage += 0.08f;
		player.magicDamage += 0.08f;
		player.minionDamage += 0.08f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "XenanisFlesh", 8);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddIngredient(225, 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
