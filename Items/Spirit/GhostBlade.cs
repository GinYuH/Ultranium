using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Phantom's Razor");
		// ((ModItem)this).Tooltip.SetDefault("Throws out phantom razors");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.damage = 67;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.useTime = 17;
		((ModItem)this).Item.useAnimation = 17;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 55, 50);
		((ModItem)this).Item.rare = 8;
		((ModItem)this).Item.UseSound = SoundID.Item69;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("GhostBlade").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.alpha = 60;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[((ModItem)this).Item.shoot] < 10;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(3261, 12);
		val.AddTile(412);
		val.Register();
	}
}
