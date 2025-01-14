using Agar.io_sfml.GameObjects;
using Agar.io_sfml.Input;
using SFML.Graphics;
using SFML.System;

public class Enemy : GameObject
{
    private CircleShape shape;
    private EnemyInput inputHandler;
    public float Radius { get; private set; }
    private Vector2f direction;
    private const float Speed = 200f;

    private List<GameObject> gameObjects;

    public Enemy(Vector2f position, float initialSize)
    {
        Radius = initialSize;
        Position = position;

        shape = new CircleShape(Radius)
        {
            FillColor = Color.Red,
            Origin = new Vector2f(Radius, Radius),
            Position = Position
        };

        inputHandler = new EnemyInput();
    }
    public void SetGameObjects(List<GameObject> objects)
    {
        gameObjects = objects;
    }

    public override void Update(float deltaTime)
    {
        if (gameObjects != null)
        {
            direction = inputHandler.GetDirection(Position, Radius, gameObjects);
            Position += direction * Speed * deltaTime;
            shape.Position = Position;
        }
    }

    public override void Render(RenderWindow window)
    {
        window.Draw(shape);
    }

    public void Grow(float amount)
    {
        SetRadius(Radius + amount / (1 + Radius * 0.1f));
    }

    public void SetRadius(float newRadius)
    {
        Radius = MathF.Max(newRadius, 0);
        shape.Radius = Radius;
        shape.Origin = new Vector2f(Radius, Radius);
    }

    public float GetRadius() => Radius;
}

