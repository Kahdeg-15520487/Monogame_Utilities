namespace Utility
{
    public static class LayerDepth
    {
        public static float BackGround = 0.0f;
        public static float TerrainBase = 0.1f;
        public static float TerrainLower = 0.2f;
        public static float Unit = 0.5f;
        public static float TerrainUpper = 0.7f;
        public static float GuiBackground = 0.8f;
        public static float GuiLower = 0.9f;
        public static float GuiUpper = 1.0f;
    }

    public enum Direction
    {
        Void = -1, None = -1,
        NorthWest, North, NorthEast,
        West, Center, East,
        SouthWest, South, SouthEast,
        Up = 1,
        Left = 3, Right = 5,
        Down = 7
    }

    public enum HorizontalAlignment
    {
        Top, Center, Bottom
    }
    public enum VerticalAlignment
    {
        Left, Center, Right
    }

    public static class Constants
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
    }
}