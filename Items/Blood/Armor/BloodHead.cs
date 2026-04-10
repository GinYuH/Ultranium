using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class BloodHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Sanguine Face Mask");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 1, 35);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.defense = 3;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("BloodBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("BloodLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n7% increased damage, +20 max mana, and +1 max minions";
		player.meleeDamage += 0.07f;
		player.rangedDamage += 0.07f;
		player.magicDamage += 0.07f;
		player.minionDamage += 0.07f;
		player.statManaMax2 += 20;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
