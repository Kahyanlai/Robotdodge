using SplashKitSDK;

public class Robotdodge
{
    public Player _player; //player object 
    private Window _gameWindow; //game window
    private List<Robot> _robots; //robot object
    private SplashKitSDK.Timer _scoreTimer; //timer object
    private List<Bullet> _bullet; //bullet object

    public bool Quit //property to check if the player has quit the game
    {
        get 
        {
            return _player.quit; 
        }
    }

    public Robotdodge(Window gamewindow)//constructor to initialize the game
    {
        _gameWindow = gamewindow;
        _player = new Player(gamewindow);
        _robots = new List<Robot>();
        SplashKit.LoadBitmap("Heart", "small.jpeg"); //load the heart image for displaying player lives
        _scoreTimer = new SplashKitSDK.Timer("Score Timer"); // Initialize the score timer
        _scoreTimer.Start();
    }

    public void HandleInput() //Handle player input
    {
        _player.HandleInput();

        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D mousePosition = SplashKit.MousePosition();
            _player.Shoot(mousePosition); //if left button of mouse is click, shoot the bullet
        }
    }

    //update game 
    public void Update()
    {

        // update each robot position
        foreach (Robot robot in _robots)
        {
            robot.Update();
        }

        // randomly add new robots 
        if (SplashKit.Rnd() < 0.008)//threshold value to control the frequency of robot spawning
        {
            _robots.Add(RandomRobot());
        }

       // ensure player stays on window and check for collisions
       _player.StayOnWindow(_gameWindow);
       _player.UpdateBullets();
       CheckCollisions();
       CheckBulletCollisions();

       if (_scoreTimer > 1000)
       {
        _player.IncreaseScore();//increase player score for every second that passes
       }
    }
    

    //check for collision betweenn player and robot
    private void CheckCollisions()
    {
        List<Robot> robotsToRemove = new List<Robot>();

        // Loop through each robot in the _robots list
        foreach (Robot robot in _robots) 
        {
            //check if the robot collides with the player or is offscreen
            if (_player.ColliedWith(robot) || robot.IsOffscreen(_gameWindow))
            {
                robotsToRemove.Add(robot);
            }

            if (_player.ColliedWith(robot))
            {
                _player.LoseLife(); // Reduce the player lives when colliding with a robot
                robotsToRemove.Add(robot);
            }
        }

        // Loop through each robot in the robots ToRemove list, remove collided or offscreen robots.
        foreach (Robot robot in robotsToRemove)
        {
            _robots.Remove(robot); //tell robots remove current robot
        }
    }

    private void CheckBulletCollisions() //method detecting collisions between bullets and robot
    {
        List<Robot> robotsToRemove = new List<Robot>();
        List<Bullet> bulletsToRemove = new List<Bullet>();

        foreach (Bullet bullet in _player.Bullets)
        {
            foreach (Robot robot in _robots)
            {
                //check if the bullet collides with a robot
                if (SplashKit.CirclesIntersect(bullet.CollisionCircle, robot.CollisionCircle))  //checks if two circles intersect or overlap.
                {
                    robotsToRemove.Add(robot);
                    break; // Exit the inner loop after the bullet collides with a robot
                }
            }
        }

        //removes the collided robots from the _robots list to ensures that robots hit by bullets are removed from the game.
        foreach (Robot robot in robotsToRemove)
        {
            _robots.Remove(robot);
        }
    }

    public void Draw()//draw game on the window
    {
        _gameWindow.Clear(Color.White);//clear the screen
        foreach (Robot robot in _robots)// draw each robot
        {
            robot.Draw();
        }
        _player.Draw();//draw player
        DrawLive();
        DrawScore();
        _player.DrawBullets();
        _gameWindow.Refresh(60); 
    }

    //create new robot at a random position
    public Robot RandomRobot()
    {
        return new Robot(_gameWindow, _player);

    }    

    //method to draw player lives
    private void DrawLive()
    {
        const int HEART_SIZE = 20; // heart size 
        const int HEART_SPACING = 30; // spacing between hearts
        const int HEART_Y = 10; //Y coordinate
        const int HEART_X_START = 0; // X-coordinate for the first heart

        for (int i = 0; i < _player.Live; i++) //loop and draw heart
        {
            int heartX = HEART_X_START + i * (HEART_SIZE + HEART_SPACING);

            //draw the heart image at the specified position
            SplashKit.DrawBitmap("Heart", heartX, HEART_Y);
        }
    }

    //method to draw player score
    private void DrawScore()
    {
        int score = _player.Score;
        string scoretext = $"Score:{score}";
        Color textColor = Color.Black;
        Font font = SplashKit.FontNamed("Times");
        int fontSize = 20;
        double scoreX = _gameWindow.Width - 100; // X-coordinate 
        double scoreY = 10; // Y-coordinate 

        //draw player's score at the specified position and font style
        SplashKit.DrawText(scoretext, Color.Black, font, fontSize, scoreX, scoreY);
    }

}
