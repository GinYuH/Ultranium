using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class TendrilKnife : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Tendril Piercer");
		// Tooltip.SetDefault("Throws tendril blades that can pierce through enemies");
	}

	public override void SetDefaults()
	{
		Item.damage = 14;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 24;
		Item.height = 24;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 1, 50);
		Item.rare = 2;
		Item.UseSound = SoundID.Item7;
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("TendrilKnife").Type;
		Item.shootSpeed = 10f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (Utils.NextBool(Main.rand, 3))
		{
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
		}
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.Register();
	}
}
