namespace Core
{
    public class BoardCoordinates
    {
        public int X { get; set; }
        
        public int Y { get; set; }

        public BoardCoordinates()
        {
            
        }

        public BoardCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}