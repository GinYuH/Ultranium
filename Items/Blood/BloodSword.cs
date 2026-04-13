using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodSword : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Bloodthorn Blade");
		//Tooltip.SetDefault("Shoots a random spread of blood thorns");
	}

	public override void SetDefaults()
	{
		Item.scale = 1.2f;
		Item.damage = 12;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item7;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("BloodThorn").Type;
		Item.shootSpeed = 5f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (Utils.NextBool(Main.rand, 2))
		{
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num = 1 + Main.rand.Next(2);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(20f));
			float num2 = 1f - Main.rand.NextFloat() * 0.3f;
			vector *= num2;
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
