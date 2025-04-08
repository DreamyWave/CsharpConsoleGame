using System;

namespace ConsoleGame;
//https://nowitzki.tistory.com/10 참고자료 
public class BSPMapGenerator
{
    private const int MinRoomSize = 15;
    private const int MaxDepth = 4;
    
    private int mWidth, mHeight;
    private Random mRandom = new Random();
    private List<Room> mRooms = new();
    
    public BSPMapGenerator(int width, int height)
    {
        mWidth = width;
        mHeight = height;
    }
    
    /// <summary>
    /// 맵 생성 실패유무 리턴
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public bool GenerateMap(ref char[,] map)
    {
        //상위 노드
        BSPNode root = new BSPNode(0, 0, mWidth, mHeight);
        SplitNode(root, 0);

        map = new char[mWidth, mHeight];

        for(int x = 0; x < mWidth; ++x)
        {
            for(int y = 0; y < mHeight; ++y)
            {
                map[x, y] = '#';
            }
        }
        
        foreach(Room room in mRooms)
        {
            for(int x = room.X; x < room.X + room.Width - 1; ++x)
            {
                for(int y = room.Y; y < room.Y + room.Height - 1; ++y)
                {
                    map[x, y] = '.';
                }
            }
        }

        return true;
    }
    //깊이 + 크기 기준으로 나눠준다 (너무 작게 잘라지는것을 방지한다.)
    private void SplitNode(BSPNode node, int depth)
    {
        if(depth >= MaxDepth || node.Width < MinRoomSize * 2 || node.Height < MinRoomSize *2)   
        {
            //자른 노드의 크기가 너무 작거나 깊이 이하면 그 노드의 내용물 기준으로 방을 만든다.
            Room room = CreateRoom(node);
            node.Room = room;
            mRooms.Add(room);
            return;
        }
        
        bool splitHorizontally = node.Width < node.Height;
        if(mRandom.NextDouble() > 0.5) splitHorizontally = !splitHorizontally;
        
        if(splitHorizontally)
        {
            int split = mRandom.Next(MinRoomSize, node.Height - MinRoomSize);
            node.Left = new BSPNode(node.X, node.Y, node.Width, split);
            node.Right = new BSPNode(node.X, node.Y + split, node.Width, node.Height - split);
        }
        else
        {
            int split = mRandom.Next(MinRoomSize, node.Width - MinRoomSize);
            node.Left = new BSPNode(node.X, node.Y, split, node.Height);
            node.Right = new BSPNode(node.X + split, node.Y, node.Width - split, node.Height);
        }
        
        SplitNode(node.Left, depth + 1);
        SplitNode(node.Right, depth + 1);
    }
    
    /// <summary>
    /// MinRoomSize 크기부터 최대 크기 사이로 방을 만든다.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private Room CreateRoom(BSPNode node)
    {
        int roomWidth = mRandom.Next(MinRoomSize, node.Width);
        int roomHeight = mRandom.Next(MinRoomSize, node.Height);
        int roomX = node.X + mRandom.Next(0, node.Width - roomWidth);
        int roomY = node.Y + mRandom.Next(0, node.Height - roomHeight);
        return new Room(roomX, roomY, roomWidth, roomHeight);
    }

}

/// <summary>
/// 분할 공간
/// </summary>
public class BSPNode
{
    public int X, Y, Width, Height;
    public BSPNode Left, Right;
    public Room Room;
    public BSPNode(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}
/// <summary>
/// 방 공간
/// </summary>
public class Room 
{
    public int X, Y, Width, Height;

    public Room(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}