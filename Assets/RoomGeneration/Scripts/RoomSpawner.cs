using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
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
    private int rand;
    private bool isSpawned = false;
    private float waitTime = 3f;
    static int counter = 0;
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
        if (counter==0 || counter>=50)
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

        if (objName == "T")
        {
            rand = Random.Range(0, roomPresets.Ts.Length);
            Instantiate(roomPresets.Ts[rand], transform.position, roomPresets.Ts[rand].transform.rotation);
        }
        else if (objName == "R")
        {
            rand = Random.Range(0, roomPresets.Rs.Length);
            Instantiate(roomPresets.Rs[rand], transform.position, roomPresets.Rs[rand].transform.rotation);
        }
        else if (objName == "B")
        {
            rand = Random.Range(0, roomPresets.Bs.Length);
            Instantiate(roomPresets.Bs[rand], transform.position, roomPresets.Bs[rand].transform.rotation);
        }
        else if (objName == "L")
        {
            rand = Random.Range(0, roomPresets.Ls.Length);
            Instantiate(roomPresets.Ls[rand], transform.position, roomPresets.Ls[rand].transform.rotation);
        }
        else if (objName == "TR")
        {
            rand = Random.Range(0, roomPresets.TRs.Length);
            Instantiate(roomPresets.TRs[rand], transform.position, roomPresets.TRs[rand].transform.rotation);
        }
        else if (objName == "TB")
        {
            rand = Random.Range(0, roomPresets.TBs.Length);
            Instantiate(roomPresets.TBs[rand], transform.position, roomPresets.TBs[rand].transform.rotation);
        }
        else if (objName == "TL")
        {
            rand = Random.Range(0, roomPresets.TLs.Length);
            Instantiate(roomPresets.TLs[rand], transform.position, roomPresets.TLs[rand].transform.rotation);
        }
        else if (objName == "RB")
        {
            rand = Random.Range(0, roomPresets.RBs.Length);
            Instantiate(roomPresets.RBs[rand], transform.position, roomPresets.RBs[rand].transform.rotation);
        }
        else if (objName == "RL")
        {
            rand = Random.Range(0, roomPresets.RLs.Length);
            Instantiate(roomPresets.RLs[rand], transform.position, roomPresets.RLs[rand].transform.rotation);
        }
        else if (objName == "BL")
        {
            rand = Random.Range(0, roomPresets.BLs.Length);
            Instantiate(roomPresets.BLs[rand], transform.position, roomPresets.BLs[rand].transform.rotation);
        }
        else if (objName == "TRB")
        {
            rand = Random.Range(0, roomPresets.TRBs.Length);
            Instantiate(roomPresets.TRBs[rand], transform.position, roomPresets.TRBs[rand].transform.rotation);
        }
        else if (objName == "TRL")
        {
            rand = Random.Range(0, roomPresets.TRLs.Length);
            Instantiate(roomPresets.TRLs[rand], transform.position, roomPresets.TRLs[rand].transform.rotation);
        }
        else if (objName == "TBL")
        {
            rand = Random.Range(0, roomPresets.TBLs.Length);
            Instantiate(roomPresets.TBLs[rand], transform.position, roomPresets.TBLs[rand].transform.rotation);
        }
        else if (objName == "RBL")
        {
            rand = Random.Range(0, roomPresets.RBLs.Length);
            Instantiate(roomPresets.RBLs[rand], transform.position, roomPresets.RBLs[rand].transform.rotation);
        }
        else if (objName == "TRBL")
        {
            rand = Random.Range(0, roomPresets.TRBLs.Length);
            Instantiate(roomPresets.TRBLs[rand], transform.position, roomPresets.TRBLs[rand].transform.rotation);
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
