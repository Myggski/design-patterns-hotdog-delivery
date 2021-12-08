using UnityEngine.UIElements;

public class AchievementBadgeBuilder {
    private VisualElement _achievementBadge;
    private Label _achievementHeader;
    private Label _achievementDescription;

    public AchievementBadgeBuilder() {
        _achievementBadge = new VisualElement {
            name = "AchievementBadge"
        };

        _achievementBadge.AddToClassList("achievement-badge");
    }

    public static AchievementBadgeBuilder Init() {
        return new AchievementBadgeBuilder();
    }

    public VisualElement Build() {
        if (!ReferenceEquals(_achievementHeader, null)) {
            _achievementBadge.Add(_achievementHeader);    
        }
        
        if (!ReferenceEquals(_achievementDescription, null)) {
            _achievementBadge.Add(_achievementDescription);    
        }
        
        return _achievementBadge;
    }

    public AchievementBadgeBuilder SetHeader(string headerText) {
        _achievementHeader = new Label(headerText);
        _achievementHeader.name = "AchievementHeader";
        _achievementHeader.AddToClassList("achievement-header");

        return this;
    }

    public AchievementBadgeBuilder SetDescription(string descriptionText) {
        _achievementDescription = new Label(descriptionText);
        _achievementDescription.name = "AchievementDescription";
        _achievementDescription.AddToClassList("achievement-description");
        
        return this;
    }
}