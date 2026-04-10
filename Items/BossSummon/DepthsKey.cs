using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DepthsKey : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Depths Key");
		// ((ModItem)this).Tooltip.SetDefault("'Charged with the power of darkness'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 16;
		((Entity)(object)((ModItem)this).Item).height = 16;
		((ModItem)this).Item.useTime = 19;
		((ModItem)this).Item.useAnimation = 19;
		((ModItem)this).Item.rare = 0;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.value = 100;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.noMelee = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "AbyssRockItem", 20);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 20);
		val.Register();
	}
}
