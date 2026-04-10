using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class ShadowFlute : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(34, 166, 162),
		new Color(138, 7, 163)
	};

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Despairides");
		((ModItem)this).Tooltip.SetDefault("When played, the flute will create a phantom claw that will home in on enemies\n~Developer Item~");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 250;
		((ModItem)this).item.mana = 20;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.magic = true;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 58;
		((ModItem)this).item.useTime = 21;
		((ModItem)this).item.useAnimation = 21;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(2);
		((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/Flute");
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("PhantomClaw");
		((ModItem)this).item.shootSpeed = 15f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, -4f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.overrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
