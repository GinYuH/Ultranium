using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class TenebrisBody : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Tenebris Chestmail");
		((ModItem)this).Tooltip.SetDefault("3% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		player.meleeDamage += 0.03f;
		player.rangedDamage += 0.03f;
		player.minionDamage += 0.03f;
		player.magicDamage += 0.03f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
