using SplashKitSDK;

public class Robot
{
    //properties representing the player's position & color
    public double X {get;set;}
    public double Y {get;set;}
    public Vector2D Velocity {get;set;} //store vector
    public Color MainColor {get;set;}

    public int Width //width of robot
    {
        get { return 50; } 
    }

    public int Height //height of robot
    {
        get { return 50; } 
    }

    // property represent the collision circle used for detecting collisions w player. 
    public Circle CollisionCircle {get; private set;}

    //constructor to initialize a robot
    public Robot(Window gamewindow, Player player)
    {   
        const int SPEED = 4;
        
        if (SplashKit.Rnd() < 0.5) //randomly choose initial position for the robot either off-screen on the top/bottom/left/right 
        {
            X = SplashKit.Rnd(gamewindow.Width); //set robot x coordinate to a random position within the width of gamewindow

            if(SplashKit.Rnd() < 0.5) //determine whether robot will start above or below the screen
                Y = -Height; //places robot above top edge of the screen, initially invisible
            else
                Y = gamewindow.Height;//place robot below the edge of screen
        }
        else
        {
            Y = SplashKit.Rnd(gamewindow.Height);//set robot y coordinate to a random position within the height of gamewindow

            if(SplashKit.Rnd() < 0.5) //places robot left sidde of screen
                X = -Width;
            else
                X = gamewindow.Width;//place robot right side of screen
        }

        MainColor = Color.RandomRGB(200); //randomly assign color of the robot. 
        CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);//create a collision circle for the robot. the robot's position is centered with a radius of 20pixels. 

        //Get a point from the robot()
        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        //Get a point from the player
        Point2D toPt = new Point2D()
        {
            X = player.X, 
            Y = player.Y
        };

        //calculate the direction from robot position to player position.
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        
        //Set the speed and assign to the velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    //draw the robot 
    public void Draw()
    {
        double leftX = X + 12;
        double rightX = X + 27; 
        double eyeY = Y + 10; 
        double mouthY = Y + 30;

        SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height); //draw the robot body (gray color)
        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10); //draw the robot left eye
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10); //draw the robot right eye
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10); //draw outer part of robot mouth
        SplashKit.FillRectangle(MainColor, leftX+2, mouthY+2, 21, 6); //draw inner part of robot left eye
    }

    //move the robot by the amount in its velocity property
    public void Update()
    {
        X += Velocity.X; //vector created point to the player, Robot head towards player 
        Y += Velocity.Y;
        CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);

    }

    //check if the robot if offscreen
    public bool IsOffscreen(Window screen)
    {
        return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
    }

}