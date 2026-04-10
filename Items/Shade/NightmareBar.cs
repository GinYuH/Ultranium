using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class NightmareBar : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Tenebris Alloy");
		// ((ModItem)this).Tooltip.SetDefault("'A dark metal from a nightmarish world'");
		Main.RegisterItemAnimation(((ModItem)this).Item.type, new DrawAnimationVertical(5, 5));
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((Entity)(object)((ModItem)this).Item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).Item).height = ((Entity)(object)item).height;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.value = 1000;
		((ModItem)this).Item.rare = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "ShadowOre", 2);
		val.AddIngredient((Mod)null, "ShadowEssence", 1);
		val.AddTile(17);
		val.Register();
	}
}
