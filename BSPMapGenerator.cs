using System;
using System.Drawing;

namespace ConsoleGame;
//https://nowitzki.tistory.com/10 참고자료 
public class BSPMapGenerator
{
    private const int MinRoomSize = 15;
    private const int MaxDepth = 5;
    
    private int mWidth, mHeight;
    private Random mRandom = new Random();
    private List<Room> mRooms = new();

    private char[,] _map;
    
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

        _map = map = new char[mWidth, mHeight];

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
        ConnectRooms(root);
        //통로연결추가
        return true;
    }

    private void ConnectRooms(BSPNode node)
    {
        if(node.Left == null || node.Right == null) return;

        //1. 자식 노드 연결
        ConnectRooms(node.Left);
        ConnectRooms(node.Right);
        // 2. 현재 노드의 좌우 방 연결
        Room roomA = GetRoom(node.Left);
        Room roomB = GetRoom(node.Right);
        if (roomA != null && roomB != null)
            CreateCorridorBetween(roomA, roomB);
    }

    private Room GetRoom(BSPNode node)
    {
        if(node == null) return null;
        if(node.Room != null) return node.Room;

        Room leftRoom = GetRoom(node.Left);
        if(leftRoom != null) return leftRoom;

        return GetRoom(node.Right);
    }

    private void CreateCorridorBetween(Room a, Room b)
    {
        if(mRandom.Next(2) == 0) {
            CreateHorizontalTunnel(a.Center.X, b.Center.X, a.Center.Y);
            CreateVerticalTunnel(a.Center.Y, b.Center.Y, b.Center.X);
        } else {
            CreateVerticalTunnel(a.Center.Y, b.Center.Y, a.Center.X);
            CreateHorizontalTunnel(a.Center.X, b.Center.X, b.Center.Y);
        }
    }

    private void CreateHorizontalTunnel(int x1, int x2, int y) {
        for(int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++) {
            if(x >= 0 && x < mWidth && y >= 0 && y < mHeight)
                _map[x, y] = '.';
        }
    }

    private void CreateVerticalTunnel(int y1, int y2, int x) {
        for(int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++) {
            if(x >= 0 && x < mWidth && y >= 0 && y < mHeight)
                _map[x, y] = '.';
        }
    }

    //깊이 + 크기 기준으로 나눠준다 (너무 작게 잘라지는것을 방지한다.)
    private void SplitNode(BSPNode node, int depth)
    {
        const float SplitRatio = 0.4f; //분할 비율을 
        if(depth >= MaxDepth || node.Width < MinRoomSize * 2 || node.Height < MinRoomSize *2)   
        {
            //자른 노드의 크기가 너무 작거나 깊이 이하면 그 노드의 내용물 기준으로 방을 만든다.
            Room room = CreateRoom(node);
            node.Room = room;
            mRooms.Add(room);
            return;
        }
        
        bool splitHorizontally = node.Width < node.Height;
        
        
        if(splitHorizontally)
        {
            int minSplit = (int)(node.Height * SplitRatio);
            if (minSplit <= MinRoomSize)
            {
                Room room = CreateRoom(node);
                node.Room = room;
                mRooms.Add(room);
                return;
            }
            int maxSplit = node.Height - minSplit;
            int split = mRandom.Next(minSplit, maxSplit);
            node.Left = new BSPNode(node.X, node.Y, node.Width, split);
            node.Right = new BSPNode(node.X, node.Y + split, node.Width, node.Height - split);
        }
        else
        {
            int minSplit = (int)(node.Width * SplitRatio);
            if (minSplit <= MinRoomSize)
            {
                Room room = CreateRoom(node);
                node.Room = room;
                mRooms.Add(room);
                return;
            }
            int maxSplit = node.Width - minSplit;
            int split = mRandom.Next(minSplit, maxSplit);
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
        int roomWidth = mRandom.Next(MinRoomSize, node.Width + 1);
        int roomHeight = mRandom.Next(MinRoomSize, node.Height + 1);
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
    //중심좌표 추가
    public Point Center => new Point(X + Width/2, Y + Height/2);

    public Room(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}