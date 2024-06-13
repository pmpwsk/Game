using uwap.Game;


//save original background color
ConsoleColor background = Console.BackgroundColor;

//set map properties
int width = 20;
int height = 10;
int[,] map = new int[width, height]; //x,y
Position player = new(0, 0);
Position target = new(width - 1, height - 1);

//set map to empty fields
for (int y = 0; y < height; y++)
    for (int x = 0; x < width; x++)
        map[x,y] = -1;

//create two walls
for (int y = 0; y < height - 2; y++)
    map[7,y] = 1;
for (int y = 2; y < height; y++)
    map[12,y] = 1;

//place player
map[player.X, player.Y] = 0;

//place target
map[target.X, target.Y] = 2;

//draw map for the first time
Console.Clear();
Console.WriteLine("Level 1");
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
        DrawField(new(x, y));

    Console.BackgroundColor = background;
    Console.WriteLine();
}

//move player until the program stops
while (true)
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.RightArrow:
            MovePlayer(new(player.X + 1, player.Y));
            break;
        case ConsoleKey.LeftArrow:
            MovePlayer(new(player.X - 1, player.Y));
            break;
        case ConsoleKey.DownArrow:
            MovePlayer(new(player.X, player.Y + 1));
            break;
        case ConsoleKey.UpArrow:
            MovePlayer(new(player.X, player.Y - 1));
            break;
    }
}

void DrawField(Position position, int? content = null)
{
    //set cursor position
    Console.CursorLeft = position.X * 2;
    Console.CursorTop = position.Y + 1;

    //draw field
    switch (content ?? map[position.X, position.Y])
    {
        case -1: //floor
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("  ");
            break;
        case 0: //player
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("  ");
            break;
        case 1: //wall
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("  ");
            break;
        case 2: //target
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
            break;
        default:
            Console.BackgroundColor = background;
            Console.Write("??");
            break;
    }
}

void SetField(Position position, int content)
{
    //set field content
    map[position.X, position.Y] = content;

    //draw field
    DrawField(position, content);
}

void ResetCursor()
{
    //set cursor position
    Console.CursorLeft = 0;
    Console.CursorTop = height + 1;

    //reset background color
    Console.BackgroundColor = background;
}

void MovePlayer(Position newPosition)
{
    //check if the new position is possible
    if (0 <= newPosition.X && newPosition.X < width && 0 <= newPosition.Y && newPosition.Y < height)
        switch (map[newPosition.X, newPosition.Y])
        {
            case -1: //empty
                SetField(player, -1);
                player = newPosition;
                SetField(player, 0);
                ResetCursor();
                break;
            case 2: //target
                SetField(player, -1);
                player = newPosition;
                SetField(player, 0);
                ResetCursor();
                Console.WriteLine("Done!");
                Environment.Exit(0);
                break;
        }
}