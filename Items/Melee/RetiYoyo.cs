using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class RetiYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Retina Throw");
		//Tooltip.SetDefault("Fires Lasers at nearby enemies");
	}

	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.width = 30;
		Item.height = 26;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.shoot = Mod.Find<ModProjectile>("RetiYoyo").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 49;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = ItemRarityID.Pink;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.HallowedBar, 10);
		val.AddIngredient(ItemID.SoulofSight, 5);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
