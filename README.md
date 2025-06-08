# 🤖 Robot Dodge Game (Enhanced Version)

A 2D game built with C# and SplashKit that challenges players to dodge robots, track their score, and shoot down enemies using bullets. This enhanced version introduces lives, scoring, and shooting mechanics to increase interactivity and gameplay depth.

## 🎮 Features

### ❤️ Player Lives
- Players start with **5 lives**
- Each collision with a robot reduces 1 life
- Remaining lives are displayed using **heart icons** on the screen
- The game ends when the player runs out of lives

### 🧮 Score System
- Score increases by **1 point every second** the player survives
- Score is displayed in real-time on the game window
- Implemented using `SplashKitTimer`

### 🔫 Bullet Shooting
- Player can **shoot bullets** by clicking the mouse
- Bullets travel from the player’s center toward the mouse click point
- Robots hit by bullets are destroyed and removed from the game

## 🧱 Key Classes and Logic

### 🔸 `Player` Class
- Tracks player position, score, and remaining lives
- Maintains a list of bullets
- `Shoot()` method fires a bullet toward the mouse
- `LoseLife()` and `IsAlive()` handle life management
- `IncreaseScore()` updates the score over time

### 🔸 `Bullet` Class
Models individual bullets with:
- **Properties**: Position, angle, speed, radius, active status
- **Methods**:
  - `Update()`: Updates bullet position using its direction
  - `Draw()`: Renders bullet as a red circle
  - `CollisionCircle()`: Returns a circle for collision detection

### 🔸 `RobotDodge` Class
- Handles game loop and rendering
- `CheckBulletCollisions()` detects collisions between robots and bullets using `CirclesIntersect()`
- Robots and bullets are removed upon collision
- `DrawLives()` and `DrawScore()` render lives and score on screen

## 🛠️ Technologies Used
- C#
- SplashKit SDK
- .NET Console Application

## 🚀 Getting Started

1. Install [SplashKit](https://splashkit.io/get-started/)
2. Clone this repository
3. Open the project folder in your C# IDE
4. Run the project:
   ```bash
   dotnet run
