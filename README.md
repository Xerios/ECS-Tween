# ECS-Tween
A very simple Unity tweening engine using pure ECS that works with GameObjects!

This is still very early stage, and it only supports position and rotation tweening, and two easing types ( Linear and ExpIn )
Rotation and Position can be used simulatinously.

![Main screenshot](/Screenshots/main.png)
![Entities](/Screenshots/entities.png)

# Example
```csharp
float time = 10f; // 10 seconds
Tween.Position(gameObject, targetPosition, time, EasingType.ExpIn);
Tween.Position(gameObject, fromPosition, toPosition, time, EasingType.Linear);
Tween.MovePosition(gameObject, translateVector, time);
Tween.Rotation(gameObject, Random.rotation,  Random.rotation, time, EasingType.ExpIn);
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

### 2. TweenEasingExpInSystem ( only used for ExpIn easing )
When _TweenEasingExpIn_ is present, we transform _TweenTime_ value after _TweenNormalizedTimeSystem_ has been executed. 
This transforms Linear time into ExpIn time value.
_( same for TweenEasingExpOutSystem )_

### 3. TweenPositionSystem / TweenRotationSystem
Interpolates position/rotation using _TweenTime_ and _TweenPosition_ and sets _Position_/_Rotation_.

### 4. TweenRemoveSystem
Handles removal of entities ( not GameObjects ) that are past their lifetime ( _TweenLifetime_ )
( Perhaps not the best decision as it detroys the entity with all its ComponentData )

### 5. TweenUpdateTransformSystem
Takes Position, Rotation and applies them to the GameObject.


# Groups

Groups are necessary to keep the correct update order, because we can't interpolate when time needs to be tweened first. Same thing for copying Entity transform to GameObject, we have to first interpolate it before we apply it to our GameObjects.

### 1. TweenTimeUpdateGroup
Main time normalization job
### 2. TweenEasingUpdateGroup
Includes all easing calculation jobs
### 3. TweenInterpolationGroup
All lerping/slerping of positions/rotations

# Contribute

I'd love to hear you out on improving this system, any contribution is welcome.
