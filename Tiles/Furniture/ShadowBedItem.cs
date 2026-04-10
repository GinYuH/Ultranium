using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowBedItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Eldritch Bed");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.rare = 0;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.value = 2000;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("ShadowBed").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "ShadowWood", 15);
		val.AddIngredient(225, 5);
		val.AddTile(18);
		val.Register();
	}
}
