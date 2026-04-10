using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Ultranium.Items.Eldritch;

public class ErebusGuitar : ModItem
{
	public int AttackMode = 1;

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eld-riff");
		((ModItem)this).Tooltip.SetDefault("'Blasting enemies with the power of eldritch music!'\nHas 3 different attack modes that can be switched by right clicking\nMode 1 will shoot green sound pulses that bounce off of tiles\nMode 2 will fire circles of fast moving purple sound pulses\nMode 3 will rapidly fire eldritch notes\nThe current mode you are in will be displayed in the corner of the item");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 270;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 12;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 15;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/ErebusGuitar1");
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ErebusGuitarPulse");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		for (int i = 0; i < 10; i++)
		{
			string text = string.Concat(AttackMode);
			new Vector2(Main.hotbarScale[i], Main.hotbarScale[i]);
			if (Main.player[Main.myPlayer].inventory[i] == ((ModItem)this).item)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Turquoise, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
			}
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			AttackMode++;
			if (AttackMode > 3)
			{
				AttackMode = 1;
			}
			Main.PlaySound(12, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);
			switch (AttackMode)
			{
			case 1:
				((ModItem)this).item.damage = 320;
				((ModItem)this).item.magic = true;
				((ModItem)this).item.mana = 12;
				((Entity)(object)((ModItem)this).item).width = 28;
				((Entity)(object)((ModItem)this).item).height = 32;
				((ModItem)this).item.useTime = 15;
				((ModItem)this).item.useAnimation = 45;
				((ModItem)this).item.useStyle = 5;
				((ModItem)this).item.noMelee = true;
				((ModItem)this).item.knockBack = 5f;
				((ModItem)this).item.rare = 11;
				((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/ErebusGuitar1");
				((ModItem)this).item.autoReuse = true;
				((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ErebusGuitarPulse");
				((ModItem)this).item.shootSpeed = 13f;
				break;
			case 2:
				((ModItem)this).item.damage = 320;
				((ModItem)this).item.magic = true;
				((ModItem)this).item.mana = 12;
				((Entity)(object)((ModItem)this).item).width = 28;
				((Entity)(object)((ModItem)this).item).height = 32;
				((ModItem)this).item.useTime = 60;
				((ModItem)this).item.useAnimation = 60;
				((ModItem)this).item.useStyle = 5;
				((ModItem)this).item.noMelee = true;
				((ModItem)this).item.knockBack = 5f;
				((ModItem)this).item.rare = 11;
				((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/ErebusGuitar2");
				((ModItem)this).item.autoReuse = true;
				((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ErebusGuitarPulse");
				((ModItem)this).item.shootSpeed = 10f;
				break;
			case 3:
				((ModItem)this).item.damage = 320;
				((ModItem)this).item.magic = true;
				((ModItem)this).item.mana = 12;
				((Entity)(object)((ModItem)this).item).width = 28;
				((Entity)(object)((ModItem)this).item).height = 32;
				((ModItem)this).item.useTime = 12;
				((ModItem)this).item.useAnimation = 120;
				((ModItem)this).item.useStyle = 5;
				((ModItem)this).item.noMelee = true;
				((ModItem)this).item.knockBack = 5f;
				((ModItem)this).item.rare = 11;
				((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/ErebusGuitar3");
				((ModItem)this).item.autoReuse = true;
				((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ErebusGuitarPulse");
				((ModItem)this).item.shootSpeed = 10f;
				break;
			default:
				return ((ModItem)this).CanUseItem(player);
			}
		}
		return ((ModItem)this).CanUseItem(player);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (player.altFunctionUse != 2)
		{
			if (AttackMode == 1)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("ErebusGuitarPulse"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			else if (AttackMode == 2)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector2 vector = ((float)Math.PI / 4f * (float)i).ToRotationVector2();
					vector.Normalize();
					vector *= 6f;
					Projectile.NewProjectile(player.Center.X, player.Center.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("ErebusGuitarPulsePurple"), damage, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			else if (AttackMode == 3)
			{
				int num = Main.rand.Next(2);
				if (num == 0)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("EldritchNote1"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
				if (num == 1)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("EldritchNote2"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
	}
}
