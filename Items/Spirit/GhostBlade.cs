using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Phantom's Razor");
		Tooltip.SetDefault("Throws out phantom razors");
	}

	public override void SetDefaults()
	{
		Item.noUseGraphic = true;
		Item.damage = 67;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 80;
		((Entity)(object)Item).height = 80;
		Item.useTime = 17;
		Item.useAnimation = 17;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 55, 50);
		Item.rare = 8;
		Item.UseSound = SoundID.Item69;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("GhostBlade").Type;
		Item.shootSpeed = 16f;
		Item.alpha = 60;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 10;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(3261, 12);
		val.AddTile(412);
		val.Register();
	}
}
