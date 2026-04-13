using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Hallow2 : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("True Chaos Blade");
		Tooltip.SetDefault("Fires a Chaos star\nHas a 20% chance to fire out a large chaos blast\nThe chaos blast will home and deal double the swords base damage");
	}

	public override void SetDefaults()
	{
		Item.damage = 80;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 84;
		Item.height = 84;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 7;
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("BlueStar").Type;
		Item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y);
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("HallowBlast").Type, 160, knockback, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "Hallow", 1);
		val.AddIngredient(549, 5);
		val.AddIngredient(548, 5);
		val.AddIngredient(547, 5);
		val.AddIngredient(502, 25);
		val.AddTile(134);
		val.Register();
	}
}
