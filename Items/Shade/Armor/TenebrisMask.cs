using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class TenebrisMask : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Tenebris Mask");
		((ModItem)this).Tooltip.SetDefault("2% increased summon damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("TenebrisBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("TenebrisLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "+2 max minions and 4% increased summon damage";
		player.maxMinions += 2;
		player.minionDamage += 0.04f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.minionDamage += 0.02f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareBar", 8);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
