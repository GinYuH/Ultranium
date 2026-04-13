using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Sickle");
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)Item).width = 40;
		((Entity)(object)Item).height = 40;
		Item.useTime = 45;
		Item.useAnimation = 32;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShadowBladeImage").Type;
		Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
