using SplashKitSDK;

//define player class
public class Player
{
    private Bitmap _PlayerBitmap;
    private List<Bullet> _bullet; //list of bullets fired by the player
    

    //properties representing the player's position and quitting status
    public double X {get; private set;}
    public double Y {get; private set;}
    public bool quit {get; private set;}

    private int SPEED = 5; //initial speed of the player
    private const int BOOSTSPEED = 15; //speed when boost
    private const int GAP = 10; //gap from the window edges to keep the player
    private int _live = 5; //field to store player life
    private const int max_live = 5;//maximum num of life
    private int _score;//player score

    // width of player
    public int Width
    {
        get
        {
            return _PlayerBitmap.Width;
        }
    
    }

    // height of player
    public int Height
    {
        get
        {
            return _PlayerBitmap.Height;
        }
    
    }

    //live of player
    public int Live
    {
        get
        {
            return _live;
        }
    
    }

    //score of player
    public int Score
    {
        get
        {
            return _score;
        }
    }

    //bullet fire by the player
    public List<Bullet> Bullets
    {
        get 
        { 
            return _bullet; 
        }
    }

    //constructor to initialize the player
    public Player(Window gameWindow)
    {
        //create a new Bitmap for player
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        //set initial position of the player at the center of the window
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;
        _live = max_live;// Initialize the player's lives to the maximum value
        _bullet = new List<Bullet>();//initialize the list of bullet
        quit = false;//set quit to false initially
    }

    public void Shoot(Point2D mousePosition)
    {
        // Calculate the angle between the player's position and the mouse position
        double angle = SplashKit.VectorAngle(SplashKit.VectorPointToPoint(SplashKit.PointAt(X, Y), mousePosition));
        
        // Create a new bullet with the calculated angle
        Bullet bullet = new Bullet(X + Width / 2, Y + Height / 2, angle);//places the bullet at the center of the player
        _bullet.Add(bullet); //create bullet to a list of bullets 
    }

    //update the position of bullets fired by the player
    public void UpdateBullets()
    {
        foreach (Bullet bullet in _bullet)
        {
            bullet.Update();
        }
    }

    //draw the bullets fired by the player
    public void DrawBullets()
    {
        foreach (Bullet bullet in _bullet)
        {
            bullet.Draw();
        }
    }


    public void Draw()
    {   
        //draw player bitmap on the current window
        _PlayerBitmap.Draw(X, Y);
    }  

    //Handle player input for movement and quitting the game
    public void HandleInput()
    {
        //check if left shift key is press
        if (SplashKit.KeyDown(KeyCode.LeftShiftKey))
        {
            SPEED = BOOSTSPEED; //boost when the user simultaneously presses the Left Shift key along with any movement key.
        }
        else
        {
            SPEED = 5;
        }
        //Check for movement keys (arrow keys) and quit key (Escape key)
        if ( SplashKit.KeyDown(KeyCode.DKey) ) X += SPEED;
        if ( SplashKit.KeyDown(KeyCode.AKey) ) X -= SPEED;
        if ( SplashKit.KeyDown(KeyCode.WKey) ) Y -= SPEED;
        if ( SplashKit.KeyDown(KeyCode.SKey) ) Y += SPEED;
        if ( SplashKit.KeyDown(KeyCode.EscapeKey) ) quit = true;
    }

    //Ensure the player stays within the window
    public void StayOnWindow(Window gameWindow)
    {   //check if the player is out of bound
        if (X < GAP)
        {
            X = GAP;
        }
        else if (X + Width > gameWindow.Width - GAP)
        {
            X = gameWindow.Width - Width - GAP;
        }
    

        if (Y < GAP)
        {
            Y = GAP;
        }
        else if (Y + Height > gameWindow.Height - GAP)
        {
            Y = gameWindow.Height - Height - GAP;
        }
    }

    //check if the player has collided with the robot
    public bool ColliedWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }

    public void LoseLife() // Add a method to reduce the player's lives when colliding with a robot
    {
        _live = _live - 1;
    }

    public bool IsAlive() // Add a method to check if the player is still alive
    {
        return _live > 0;
    }

    public void IncreaseScore() // Add a method to increase player score
    {
        _score = _score + 1;
    }
} 
         
    


        
    