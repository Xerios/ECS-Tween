# ECS-Tween
A very simple Unity tweening engine using pure ECS that works with GameObjects!

This is still very early stage, and it only supports position tweening, and two easing types ( Linear and ExpIn )

![Main screenshot](/Screenshots/main.png)
![Entities](/Screenshots/entities.png)

# Example
```csharp
Tween.Position(gameObject, targetPosition, 10f, EasingType.ExpIn);
Tween.Position(gameObject, fromPosition, toPosition, 10f, EasingType.Linear);
Tween.MovePosition(gameObject, translateVector, 10f);
```

# How does it work?
The idea behind this is to harvest ECS architecture to execute thousands of tweens efficiently.

By using one of the tweening functions, we link an Entity to GameObject ( using _AddToEntityManager_ ) and then include the following ComponentData types:
* **TweenLifetime**: Contains Start Time, and Lifetime ( tween time )
* **TweenTime**: Represents normalized time, calculated using current time and TweenLifetime values
* **TweenPosition**: contains current position and target position to interpolate

The following ComponentData types are required to update GameObject's transform.
* **Position**: Required for CopyTransformToGameObject
* **CopyTransformToGameObject**: Required to update GameObject's transform from Position

# The flow

### 1. TweenNormalizedTimeSystem
We first normalize time from using _TweenTime_ and _TweenLifetime_, this is handled by this system. _TweenTime_ is the final normalized result.

### 2. TweenEasingExponentialSystem ( only used for ExpIn easing )
When _TweenEasingExpIn_ is present, we transform _TweenTime_ value after _TweenNormalizedTimeSystem_ has been executed. 
This transforms Linear time into ExpIn time value.

### 3. TweenPositionSystem
Interpolates position using _TweenTime_ and _TweenPosition_ and sets _Position_.

### 4. TweenRemoveSystem
Handles removal of entities ( not GameObjects ) that are past their lifetime ( _TweenLifetime_ )
( Perhaps not the best decision as it detroys the entity with all its ComponentData )

### 5. Unity.Transforms.CopyTransformToGameObjectSystem
Takes Position, Rotation, LocalPosition, LocalRotation and applies them to the GameObject.

# Contribute

I'd love to hear you out on improving this system, any contribution is welcome.
