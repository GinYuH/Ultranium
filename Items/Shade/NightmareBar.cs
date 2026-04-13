using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class NightmareBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Alloy");
		//Tooltip.SetDefault("'A dark metal from a nightmarish world'");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		Item.value = Item.buyPrice(0, 10);
		Item.width = Item.width;
		Item.height = Item.height;
		Item.maxStack = Item.CommonMaxStack;
		Item.value = 1000;
		Item.rare = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowOre", 2);
		val.AddIngredient((Mod)null, "ShadowEssence", 1);
		val.AddTile(17);
		val.Register();
	}
}
