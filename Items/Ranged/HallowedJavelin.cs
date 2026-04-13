using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class HallowedJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Holy Javelin");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 50;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 62;
		((Entity)(object)Item).height = 60;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Pink;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("HallowedJavelin").Type;
		Item.shootSpeed = 11.5f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.HallowedBar, 12);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
