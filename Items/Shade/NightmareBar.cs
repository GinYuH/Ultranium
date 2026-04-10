using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class NightmareBar : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Tenebris Alloy");
		((ModItem)this).Tooltip.SetDefault("'A dark metal from a nightmarish world'");
		Main.RegisterItemAnimation(((ModItem)this).item.type, new DrawAnimationVertical(5, 5));
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((Entity)(object)((ModItem)this).item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).item).height = ((Entity)(object)item).height;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.value = 1000;
		((ModItem)this).item.rare = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "ShadowOre", 2);
		val.AddIngredient((Mod)null, "ShadowEssence", 1);
		val.AddTile(17);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
