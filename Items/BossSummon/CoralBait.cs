using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class CoralBait : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Coral Bait");
		((ModItem)this).Tooltip.SetDefault("A strange bait made with coral shards...\nAttracts the zephyr squid, only when used as bait in the ocean\nCan be used as normal fishing bait anywhere else");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.bait = 25;
		((ModItem)this).item.consumable = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(275, 10);
		val.AddRecipeGroup("Ultranium:ShadowScale/TissueSample", 6);
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
