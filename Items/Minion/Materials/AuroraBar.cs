using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Materials;

public class AuroraBar : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Aurora Crystal Bar");
		Tooltip.SetDefault("Its glow is almost blinding to look at");
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		Item.width = Item.width;
		Item.height = Item.height;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "AuroraOreItem", 4);
		val.AddTile(17);
		val.Register();
	}
}
