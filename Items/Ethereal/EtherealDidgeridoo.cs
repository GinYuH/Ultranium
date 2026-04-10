using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealDidgeridoo : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ethereal Didgeridoo");
		// ((ModItem)this).Tooltip.SetDefault("Conujures lingering ethereal notes that create ethereal tentacles upon death");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 75;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 12;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.rare = 9;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.UseSound = ((ModItem)this).Mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/Didgeridoo");
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("EtherealNote").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-8f, 0f);
	}
}
