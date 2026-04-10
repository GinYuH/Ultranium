using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticIceStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ice Mist Staff");
		// ((ModItem)this).Tooltip.SetDefault("Shoots a giant ice ball that explodes into a circle of ice shards");
		Item.staff[((ModItem)this).Item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 100;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 22;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 56;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.rare = 8;
		((ModItem)this).Item.UseSound = SoundID.Item120;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("IceBall").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}
}
