namespace GB_Platformer
{
    public class Square
    {
        public ControlNode TopLeft;
        public ControlNode TopRight;
        public ControlNode BottomRight;
        public ControlNode BottomLeft;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomRight, ControlNode bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }
    } 
}
