using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AuroraHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Aurora Crystal Mask");
		((ModItem)this).Tooltip.SetDefault("5% increased summon damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("AuroraBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("AuroraLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n+2 max minions\nStars will fall from the sky when you are hit";
		player.starCloak = true;
		player.maxMinions += 2;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.minionDamage += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AuroraBar", 6);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
