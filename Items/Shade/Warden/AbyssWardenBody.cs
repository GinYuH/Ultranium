using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AbyssWardenBody : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Abyssal Chestmail");
		((ModItem)this).Tooltip.SetDefault("10% increased critical strike chance and +1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.defense = 22;
	}

	public override void UpdateEquip(Player player)
	{
		player.meleeCrit += 10;
		player.rangedCrit += 10;
		player.magicCrit += 10;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AbyssRockItem", 60);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 30);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
