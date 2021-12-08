# design-patterns-hotdog-delivery
This is my gradable assignment called **Codename: Hotdog Delivery** for the design pattern course at FutureGames. You play as a Food delivery driver that drives around a food truck that delivers hotdogs, and your only customers are dumpsters. This is just a ***very*** small playground/demo. 

***Coded by Tomas Wallin***

## Patterns in this project

The most commonly used pattern for me personally before this course was the Singleton pattern. 
I've also used Object pooling before, but not as commonly as I should.

It was a really fun project for me, I wanted also to try out State Machine Pattern but never got the time in this course. I'll definitely check that out later. 

### Command pattern
This can be found in `Assets/Scripts/Commands. 
I use it to control the car in the game *(see `PlayerCarInput.cs`)*. 
Because I also had in mind that maybe an AI of some sort would also be able to control the car, but in its own way. 
### Singleton pattern
This can be found in `Assets/Scripts/ObjectPoolers` inside `ObjectPooler.cs` in `class ObjectPooler` as `ObjectPooler.instance`
### Composite pattern
This can be found in `Assets/Scripts/Core/Achievements`. 

`AchievementInt.cs` is my *Leaf*, and `CombinedAchievementInt.cs` is my *Composite*. I use Scriptable Objects so you can create custom achievements.

In this project, I've created three achievements. FoodDelivered50, FoodDelivered100, and FoodDelivered150 to have a closer look at it.    

You can see them as Basic Achievement, Advanced Achievement, and Expert Achievement. You need to complete all the Basic ones before you can start on Advanced Achievement, and so on. 
The idea was to have some sort of achievement tree, you've to complete the *'leaf'* achievements before being able to complete the rest. 

### Observer pattern
This can be found in `Assets/Scripts/Core/Achievements` inside `Achievement.cs`. There's an `event Action` called **OnAchievementCompleted** that will be invoked each time an event has reached its goal. 
The subscriber can be found in `Assets/Scripts/Core/UI` inside `AchievementDisplay.cs`. 
### Builder pattern
This can be found in `Assets/Scripts/Builders` inside `AchievementBadgeBuilder.cs`. It builds an *AchievementBadge* for the UI, that displays each time an achievement has been completed. 
### Object pooling
This can be found in `Assets/Scripts/ObjectPoolers` inside `ObjectPooler.cs` in `class ObjectPooler`. It pre-creates gameobjects and uses SetActive(false) to hide them in the scene. It also uses Scriptable Objects `PoolObjectTag` as an identifier to find the gameobject that you want to spawn. 
