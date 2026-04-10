using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Eldritch Pumpkin Scepter");
		// ((ModItem)this).Tooltip.SetDefault("Summons a stationary eldritch pumpkin that shoots flaming seeds at enemies\nOnly one pumpkin sentry can be active at once");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 26;
		((Entity)(object)((ModItem)this).Item).height = 28;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.mana = 20;
		((ModItem)this).Item.damage = 45;
		((ModItem)this).Item.knockBack = 1f;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.DamageType = DamageClass.Summon;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.UseSound = SoundID.Item117;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("EldritchPumpkinSentry").Type;
		((ModItem)this).Item.shootSpeed = 0f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < Main.projectile.Length; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (((Entity)projectile).active && projectile.type == ((ModItem)this).Item.shoot && projectile.owner == player.whoAmI)
			{
				((Entity)projectile).active = false;
			}
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "PumpkinSummon", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.Register();
	}
}
