using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowChairItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Eldritch Chair");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.rare = 0;
		((Entity)(object)((ModItem)this).Item).width = 12;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.value = 150;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("ShadowChair").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "ShadowWood", 4);
		val.AddTile(18);
		val.Register();
	}
}
