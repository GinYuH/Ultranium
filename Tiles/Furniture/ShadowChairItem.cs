using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowChairItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Chair");
	}

	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.White;
		Item.width = 12;
		Item.height = 30;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 150;
		Item.createTile = Mod.Find<ModTile>("ShadowChair").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowWood", 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}
