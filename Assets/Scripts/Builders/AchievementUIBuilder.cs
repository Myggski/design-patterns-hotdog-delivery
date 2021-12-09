using UnityEngine.UIElements;

public class AchievementBadgeBuilder {
    private VisualElement _achievementBadge;
    private Label _achievementHeader;
    private Label _achievementDescription;

    private AchievementBadgeBuilder() {
        _achievementBadge = new VisualElement {
            name = "AchievementBadge"
        };

        _achievementBadge.AddToClassList("achievement-badge");
    }

    /// <summary>
    /// Creates a new instance of AchievementBadgeBuilder
    /// </summary>
    /// <returns></returns>
    public static AchievementBadgeBuilder Init() {
        return new AchievementBadgeBuilder();
    }

    /// <summary>
    /// Returns UI of a achievement badge
    /// </summary>
    /// <returns></returns>
    public VisualElement Build() {
        if (!ReferenceEquals(_achievementHeader, null)) {
            _achievementBadge.Add(_achievementHeader);    
        }
        
        if (!ReferenceEquals(_achievementDescription, null)) {
            _achievementBadge.Add(_achievementDescription);    
        }
        
        return _achievementBadge;
    }

    /// <summary>
    /// Sets the header of the achievement
    /// </summary>
    /// <param name="headerText">Text of the header</param>
    /// <returns></returns>
    public AchievementBadgeBuilder SetHeader(string headerText) {
        _achievementHeader = new Label(headerText);
        _achievementHeader.name = "AchievementHeader";
        _achievementHeader.AddToClassList("achievement-header");

        return this;
    }

    /// <summary>
    /// Sets the description of the achievement
    /// </summary>
    /// <param name="descriptionText"></param>
    /// <returns></returns>
    public AchievementBadgeBuilder SetDescription(string descriptionText) {
        _achievementDescription = new Label(descriptionText);
        _achievementDescription.name = "AchievementDescription";
        _achievementDescription.AddToClassList("achievement-description");
        
        return this;
    }
}