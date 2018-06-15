# ECS-Tween
A very simple Unity tweening engine using ECS that works with GameObjects!

This is still very early stage, and it only supports position tweening, and two easing types ( Linear and ExpIn )

![Main screenshot](/Screenshots/main.png)
![Entities](/Screenshots/entities.png)

# Example
```csharp
Tween.Position(GameObject, target, 10f);
Tween.Position(GameObject, new Vector3(-10f, 0, 0), new Vector3(10f, 0, 0), 10f);
Tween.MovePosition(GameObject, new Vector3(10f, 0, 0), 10f);
```

# How does it work?
The idea behind this is to harvest ECS architecture to execute thousands of tweens efficiently.

By using one of the tweening functions, we add an Entity to a GameObject and then include the following ComponentData types:
* **TweenLifetime**: Contains Start Time, and Lifetime ( tween time )
* **TweenTime**: Represents normalized time, calculated using current time and TweenLifetime values
* **TweenTarget**: contains current position and target position to interpolate

The following ComponentData types are required to update GameObject's transform.
* **Position**: Required for CopyTransformToGameObject
* **CopyTransformToGameObject**: Required to update GameObject's transform from Position

## TweenNormalizedTimeSystem
We first normalize time from using _TweenTime_ and _TweenLifetime_, this is handled by this system. _TweenTime_ is the final normalized result.

## TweenEasingExponentialSystem
When _TweenEasingExpIn_ is present, we transform the data after _TweenNormalizedTimeSystem_ has been executed. This transforms Linear time into ExpIn time value.

## TweenRemoveSystem
Handles removal of entities ( not GameObjects ) that are past their lifetime ( _TweenLifetime_ )

## TweenPositionSystem
Interpolates position using _TweenTime_ and _TweenTarget_ and sets _Position_.


# Contribute

I'd love to hear you out on improving this system, any contribution is welcome.