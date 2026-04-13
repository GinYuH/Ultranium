using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SanctusStella : ModItem
{
	public override void SetDefaults()
	{
		Item.damage = 53;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 28;
		Item.height = 28;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = ItemRarityID.Pink;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
		Item.shoot = Mod.Find<ModProjectile>("SkyStar").Type;
		Item.shootSpeed = 16f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.SoulofFlight, 5);
		val.AddIngredient(ItemID.HallowedBar, 6);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
