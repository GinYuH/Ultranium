using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowLanternItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Lantern");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 11;
		Item.maxStack = 999;
		Item.rare = ItemRarityID.White;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("ShadowLantern").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowWood", 6);
		val.AddIngredient(ItemID.Torch, 1);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}
