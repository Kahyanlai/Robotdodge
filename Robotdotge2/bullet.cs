using SplashKitSDK;

public class Bullet
{   
    //properties of the bullet
    private double _x, _y;
    private double _angle;
    private const int SPEED = 10;
    private const int RADIUS = 5;
    private bool _active = false;

    // Constructor initializes the bullet's properties.
    public Bullet(double x, double y, double angle)
    {
        _x = x;
        _y = y;
        _angle = angle;
        _active = true;
    }

    public Bullet()
    {
        _active = false;
    }

    // Updates the bullet's position.
    public void Update()
    {
        // Move the bullet in the direction of the angle
        Vector2D movement = SplashKit.VectorFromAngle(_angle, SPEED);
        _x += movement.X;
        _y += movement.Y;

        //if bullet is out of screen
        if ((_x > SplashKit.ScreenWidth() || _x < 0) || _y > SplashKit.ScreenHeight() || _y < 0)
            {
                _active = false;
            }
    }

    // Draws the bullet.
    public void Draw()
    {
        if (_active)
        {
        SplashKit.FillCircle(Color.Red, _x, _y, RADIUS);
        }
    }

    //colliison circle of the bullet
    public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(_x, _y, RADIUS); }
    }
}