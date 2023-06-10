using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public static int maxCounter = 5;
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left,
        TopRight,
        BottomRight,
        BottomLeft,
        TopLeft,
        TopBottom,
        RightLeft,
        None
    }

    private MultiwayRooms multiwayRooms;
    private SinglewayRooms singlewayRooms;
    private CornerRooms cornerRooms;
    private RoomPresets roomPresets;
    private RoomLayouts roomLayouts;
    private int rand;
    private bool isSpawned = false;
    private float waitTime = 3f;
    private static int counter = 0;
    private bool isTopFree = true;
    private bool isRightFree = true;
    private bool isBottomFree = true;
    private bool isLeftFree = true;

    private void Start()
    {
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 32f), 3f) is not null)
            isTopFree = false;
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x + 32f, transform.position.y), 3f) is not null)
            isRightFree = false;
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 32f), 3f) is not null)
            isBottomFree = false;
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 32f, transform.position.y), 3f) is not null)
            isLeftFree = false;
    }

    private void Update()
    {
        roomPresets = GameObject.FindGameObjectWithTag("RoomPresets").GetComponent<RoomPresets>();
        roomLayouts = GameObject.FindGameObjectWithTag("RoomLayouts").GetComponent<RoomLayouts>();
        if (counter==0 || counter>=maxCounter)
        {
            singlewayRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<SinglewayRooms>();
            cornerRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<CornerRooms>();
            Destroy(gameObject, waitTime);
            Invoke("SpawnSingle", 0.2f);
        }
        else
        {
            multiwayRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MultiwayRooms>();
            cornerRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<CornerRooms>();
            Destroy(gameObject, waitTime);
            Invoke("SpawnMultiple", 0.2f);
        }
    }

    public void SetRoomFromArray(GameObject[] arr)
    {
        rand = Random.Range(0, arr.Length);
        Instantiate(arr[rand], transform.position, arr[rand].transform.rotation);
        SetRoomPreset(arr[rand]);
    }

    public void SpawnSingle()
    {
        if (!isSpawned)
        {
            if (direction == Direction.None)
            {
                rand = Random.Range(0, 4);
                if (rand == 0)
                    this.direction = Direction.Top;
                else if (rand == 1)
                    this.direction = Direction.Right;
                else if (rand == 2)
                    this.direction = Direction.Bottom;
                else if (rand == 3)
                    this.direction = Direction.Left;
                SpawnSingle();
                return;
            }
            if (direction == Direction.Top)
                SetRoomFromArray(singlewayRooms.topRooms);

            else if (direction == Direction.Right)
                SetRoomFromArray(singlewayRooms.rightRooms);

            else if (direction == Direction.Bottom)
                SetRoomFromArray(singlewayRooms.bottomRooms);

            else if (direction == Direction.Left)
                SetRoomFromArray(singlewayRooms.leftRooms);

            else if (direction == Direction.TopRight)
                SetRoomFromArray(cornerRooms.topRightRooms);

            else if (direction == Direction.TopLeft)
                SetRoomFromArray(cornerRooms.topLeftRooms);

            else if (direction == Direction.BottomRight)
                SetRoomFromArray(cornerRooms.bottomRightRooms);

            else if (direction == Direction.BottomLeft)
                SetRoomFromArray(cornerRooms.bottomLeftRooms);

            else if (direction == Direction.TopBottom)
                SetRoomFromArray(cornerRooms.topBottomRooms);

            else if (direction == Direction.RightLeft)
                SetRoomFromArray(cornerRooms.rightLeftRooms);

            counter++;
            isSpawned = true;
        }
    }

    public void SpawnMultiple()
    {
        if (!isSpawned)
        {
            if (direction == Direction.Top)
                SetRoomFromArray(multiwayRooms.topRooms);

            else if (direction == Direction.Right)
                SetRoomFromArray(multiwayRooms.rightRooms);

            else if (direction == Direction.Bottom)
                SetRoomFromArray(multiwayRooms.bottomRooms);

            else if (direction == Direction.Left)
                SetRoomFromArray(multiwayRooms.leftRooms);

            else if (direction == Direction.TopRight)
                SetRoomFromArray(cornerRooms.topRightRooms);

            else if (direction == Direction.TopLeft)
                SetRoomFromArray(cornerRooms.topLeftRooms);

            else if (direction == Direction.BottomRight)
                SetRoomFromArray(cornerRooms.bottomRightRooms);

            else if (direction == Direction.BottomLeft)
                SetRoomFromArray(cornerRooms.bottomLeftRooms);
            
            else if (direction == Direction.TopBottom)
                SetRoomFromArray(cornerRooms.topBottomRooms);

            else if (direction == Direction.RightLeft)
                SetRoomFromArray(cornerRooms.rightLeftRooms);

            counter++;
            isSpawned = true;
        }
    }

    public void SetRoomPreset(GameObject anchor)
    {
        string objName = anchor.name;
        Direction dir = this.GetComponent<RoomSpawner>().direction;
        if (objName.Contains('T') && !isTopFree && !(dir == Direction.Top || dir == Direction.TopLeft || dir == Direction.TopRight || dir == Direction.TopBottom))
            objName = objName.Remove(objName.IndexOf('T'), 1);

        if (objName.Contains('R') && !isRightFree && !(dir == Direction.Right || dir == Direction.TopRight || dir == Direction.BottomRight || dir == Direction.RightLeft))
            objName = objName.Remove(objName.IndexOf('R'), 1);

        if (objName.Contains('B') && !isBottomFree && !(dir == Direction.Bottom || dir == Direction.BottomLeft || dir == Direction.BottomRight || dir == Direction.TopBottom))
            objName = objName.Remove(objName.IndexOf('B'), 1);

        if (objName.Contains('L') && !isLeftFree && !(dir == Direction.Left || dir == Direction.BottomLeft || dir == Direction.TopLeft || dir == Direction.RightLeft))
            objName = objName.Remove(objName.IndexOf('L'), 1);

        int chk = Random.Range(0, 2);
        if (objName == "T")
            Instantiate(roomPresets.Ts[chk], transform.position, roomPresets.Ts[chk].transform.rotation);
        else if (objName == "R")
            Instantiate(roomPresets.Rs[chk], transform.position, roomPresets.Rs[chk].transform.rotation);
        else if (objName == "B")
            Instantiate(roomPresets.Bs[chk], transform.position, roomPresets.Bs[chk].transform.rotation);
        else if (objName == "L")
            Instantiate(roomPresets.Ls[chk], transform.position, roomPresets.Ls[chk].transform.rotation);
        else if (objName == "TR")
            Instantiate(roomPresets.TRs[chk], transform.position, roomPresets.TRs[chk].transform.rotation);
        else if (objName == "TB")
            Instantiate(roomPresets.TBs[chk], transform.position, roomPresets.TBs[chk].transform.rotation);
        else if (objName == "TL")
            Instantiate(roomPresets.TLs[chk], transform.position, roomPresets.TLs[chk].transform.rotation);
        else if (objName == "RB")
            Instantiate(roomPresets.RBs[chk], transform.position, roomPresets.RBs[chk].transform.rotation);
        else if (objName == "RL")
            Instantiate(roomPresets.RLs[chk], transform.position, roomPresets.RLs[chk].transform.rotation);
        else if (objName == "BL")
            Instantiate(roomPresets.BLs[chk], transform.position, roomPresets.BLs[chk].transform.rotation);
        else if (objName == "TRB")
            Instantiate(roomPresets.TRBs[chk], transform.position, roomPresets.TRBs[chk].transform.rotation);
        else if (objName == "TRL")
            Instantiate(roomPresets.TRLs[chk], transform.position, roomPresets.TRLs[chk].transform.rotation);
        else if (objName == "TBL")
            Instantiate(roomPresets.TBLs[chk], transform.position, roomPresets.TBLs[chk].transform.rotation);
        else if (objName == "RBL")
            Instantiate(roomPresets.RBLs[chk], transform.position, roomPresets.RBLs[chk].transform.rotation);
        else if (objName == "TRBL")
            Instantiate(roomPresets.TRBLs[chk], transform.position, roomPresets.TRBLs[chk].transform.rotation);
        if (counter!=0)
            SetLayout(chk);
    }

    void SetLayout(int type)
    {
        if (type == 0)
        {
            int ind = Random.Range(0, roomLayouts.boxLayouts.Length);
            Instantiate(roomLayouts.boxLayouts[ind], transform.position, roomLayouts.boxLayouts[ind].transform.rotation);
        }
        else
        {
            int ind = Random.Range(0, roomLayouts.boxLayouts.Length);
            Instantiate(roomLayouts.circleLayouts[ind], transform.position, roomLayouts.boxLayouts[ind].transform.rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isSpawned && other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().isSpawned)
        {
            Destroy(gameObject);
        }
        if (!isSpawned && other.CompareTag("RoomPoint") && !other.GetComponent<RoomSpawner>().isSpawned)
        {
            if (this.direction == Direction.Top && other.GetComponent<RoomSpawner>().direction == Direction.Right
                || this.direction == Direction.Right && other.GetComponent<RoomSpawner>().direction == Direction.Top)
            {
                Destroy(other.gameObject);
                this.direction = Direction.TopRight;
            }
            else if (this.direction == Direction.Top && other.GetComponent<RoomSpawner>().direction == Direction.Left
                || this.direction == Direction.Left && other.GetComponent<RoomSpawner>().direction == Direction.Top)
            {
                Destroy(other.gameObject);
                this.direction = Direction.TopLeft;
            }
            else if (this.direction == Direction.Bottom && other.GetComponent<RoomSpawner>().direction == Direction.Right
                || this.direction == Direction.Right && other.GetComponent<RoomSpawner>().direction == Direction.Bottom)
            {
                Destroy(other.gameObject);
                this.direction = Direction.BottomRight;
            }
            else if (this.direction == Direction.Bottom && other.GetComponent<RoomSpawner>().direction == Direction.Left
                || this.direction == Direction.Left && other.GetComponent<RoomSpawner>().direction == Direction.Bottom)
            {
                Destroy(other.gameObject);
                this.direction = Direction.BottomLeft;
            }
            else if (this.direction == Direction.Top && other.GetComponent<RoomSpawner>().direction == Direction.Bottom
                || this.direction == Direction.Bottom && other.GetComponent<RoomSpawner>().direction == Direction.Top)
            {
                Destroy(other.gameObject);
                this.direction = Direction.TopBottom;
            }
            else if (this.direction == Direction.Right && other.GetComponent<RoomSpawner>().direction == Direction.Left
                || this.direction == Direction.Left && other.GetComponent<RoomSpawner>().direction == Direction.Right)
            {
                Destroy(other.gameObject);
                this.direction = Direction.RightLeft;
            }
        }
    }
}
