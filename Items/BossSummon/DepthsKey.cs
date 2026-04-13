using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DepthsKey : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Depths Key");
		//Tooltip.SetDefault("'Charged with the power of darkness'");
	}

	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.useTime = 19;
		Item.useAnimation = 19;
		Item.rare = ItemRarityID.White;
		Item.maxStack = Item.CommonMaxStack;
		Item.value = 100;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.noMelee = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "AbyssRockItem", 20);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 20);
		val.Register();
	}
}
