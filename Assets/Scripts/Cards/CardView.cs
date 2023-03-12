using CardSystem;
using UnityEngine;
using UnityEngine.UI;

public class CardView
{
    private Sprite typeSprite;
    private Color typeColor;
    public void ColorCard(Transform card, CardData data)
    {
        DefineTypeColorAndSprite(data.cardType);
        RepaintCardElements(card);
        ChangeImages(card, data.image);
        FillGeneralText(card, data);
    }

    private void DefineTypeColorAndSprite(CardType type) 
    {
        TypeToColorConverter palette = Resources.Load<TypeToColorConverter>("Cards/CardPalette");
        switch (type)
        {
            case (CardType.Head):
                typeSprite = palette.HeadIcon;
                typeColor = palette.HeadColor;
                break;
            case (CardType.Limbs):
                typeSprite = palette.LimbsIcon;
                typeColor = palette.LimbsColor;
                break;
            case (CardType.Mouth):
                typeSprite = palette.MouthIcon;
                typeColor = palette.MouthColor;
                break;
            case (CardType.Cover):
                typeSprite = palette.CoverIcon;
                typeColor = palette.CoverColor;
                break;
            case (CardType.InternalStructure):
                typeSprite = palette.InternalStructureIcon;
                typeColor = palette.InternalStructureColor;
                break;
        }
    }
    private void RepaintCardElements(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out IMainColorChanger colorChanger))
            {
                colorChanger.ChangeColorTo(typeColor);
            }
        }
    }

    private void ChangeImages(Transform transform, Sprite image)
    {
        TypeIcon icon = transform.GetComponentInChildren<TypeIcon>();
        icon.ChangeImage(typeSprite);

        MainImage mainImage = transform.GetComponentInChildren<MainImage>();
        mainImage.ChangeImage(image);
    }
    private void FillGeneralText(Transform transform, CardData data)
    {
        transform.GetComponentInChildren<CardTitleText>().ChangeText(
            data.title);

        transform.GetComponentInChildren<CardTypeText>().ChangeText(
            data.cardType.ToString());

        transform.GetComponentInChildren<CardDescriptionText>().ChangeText(
            GenerateDescription(data));
    }

    private string GenerateDescription(CardData data)
    {
        string description = "";

        if (data.attackBonus != 0)
        {
            description += ("+" + data.attackBonus + " Attack,\n");
        }
        if (data.defenseBonus != 0)
        {
            description += ("+" + data.defenseBonus + " Defense,\n");
        }
        if (data.moveSpeedBonus != 0)
        {
            description += ("+" + data.moveSpeedBonus + " Move speed,\n");
        }
        if (data.spawnSpeedBonus != 0)
        {
            description += ("+" + data.spawnSpeedBonus + " Spawn speed,\n");
        }
        if (data.swimSpeedTimeBonus != 0)
        {
            description += ("+" + data.swimSpeedTimeBonus + " Swim,\n");
        }
        if (data.climbSpeedBonus != 0)
        {
            description += ("+" + data.climbSpeedBonus + " Climb,\n");
        }
        if (data.coldResistanceBonus != 0)
        {
            description += ("+" + data.coldResistanceBonus + " Cold resistance,\n");
        }
        if (data.heatResistanceBonus != 0)
        {
            description += ("+" + data.heatResistanceBonus + " Heat resistance,\n");
        }
        if (data.poisonResistanceBonus != 0)
        {
            description += ("+" + data.poisonResistanceBonus + " Poison resistance ,\n");
        }

        return description;
    }
}
