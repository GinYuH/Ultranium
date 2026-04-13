using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkibomb : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Pumpki-Bomb");
		//Tooltip.SetDefault("Throws an eldritch pumpkin bomb that explodes into lingering fire\nThe pumpkin will also explode into tentacles when it hits an enemy");
	}

	public override void SetDefaults()
	{
		Item.damage = 45;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 20;
		((Entity)(object)Item).height = 20;
		Item.useTime = 38;
		Item.useAnimation = 38;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 10, 50);
		Item.rare = ItemRarityID.LightRed;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchPumpkibomb").Type;
		Item.shootSpeed = 6.5f;
		Item.useTurn = true;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "Pumpkibomb", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(ItemID.SoulofNight, 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
