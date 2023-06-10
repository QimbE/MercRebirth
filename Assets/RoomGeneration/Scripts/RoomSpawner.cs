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
        None
    }

    private MultiwayRooms multiwayRooms;
    private SinglewayRooms singlewayRooms;
    private RoomPresets roomPresets;
    private int rand;
    private bool isSpawned = false;
    private float waitTime = 3f;
    static int counter = 0;
    

    private void Start()
    {
        roomPresets = GameObject.FindGameObjectWithTag("RoomPresets").GetComponent<RoomPresets>();
        if (counter==0 || counter>=3)
        {
            singlewayRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<SinglewayRooms>();
            Destroy(gameObject, waitTime);
            Invoke("SpawnSingle", 0.2f);
        }
        else
        {
            multiwayRooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MultiwayRooms>();
            Destroy(gameObject, waitTime);
            Invoke("SpawnMultiple", 0.2f);
        }
    }

    public void SpawnSingle()
    {
        if (!isSpawned)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, singlewayRooms.topRooms.Length);
                Instantiate(singlewayRooms.topRooms[rand], transform.position, singlewayRooms.topRooms[rand].transform.rotation);
                SetRoomPreset(singlewayRooms.topRooms[rand]);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, singlewayRooms.rightRooms.Length);
                Instantiate(singlewayRooms.rightRooms[rand], transform.position, singlewayRooms.rightRooms[rand].transform.rotation);
                SetRoomPreset(singlewayRooms.rightRooms[rand]);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, singlewayRooms.bottomRooms.Length);
                Instantiate(singlewayRooms.bottomRooms[rand], transform.position, singlewayRooms.bottomRooms[rand].transform.rotation);
                SetRoomPreset(singlewayRooms.bottomRooms[rand]);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, singlewayRooms.leftRooms.Length);
                Instantiate(singlewayRooms.leftRooms[rand], transform.position, singlewayRooms.leftRooms[rand].transform.rotation);
                SetRoomPreset(singlewayRooms.leftRooms[rand]);
            }
            counter++;
            isSpawned = true;
        }
    }

    public void SpawnMultiple()
    {
        if (!isSpawned)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, multiwayRooms.topRooms.Length);
                Instantiate(multiwayRooms.topRooms[rand], transform.position, multiwayRooms.topRooms[rand].transform.rotation);
                SetRoomPreset(multiwayRooms.topRooms[rand]);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, multiwayRooms.rightRooms.Length);
                Instantiate(multiwayRooms.rightRooms[rand], transform.position, multiwayRooms.rightRooms[rand].transform.rotation);
                SetRoomPreset(multiwayRooms.rightRooms[rand]);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, multiwayRooms.bottomRooms.Length);
                Instantiate(multiwayRooms.bottomRooms[rand], transform.position, multiwayRooms.bottomRooms[rand].transform.rotation);
                SetRoomPreset(multiwayRooms.bottomRooms[rand]);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, multiwayRooms.leftRooms.Length);
                Instantiate(multiwayRooms.leftRooms[rand], transform.position, multiwayRooms.leftRooms[rand].transform.rotation);
                SetRoomPreset(multiwayRooms.leftRooms[rand]);
            }
            counter++;
            isSpawned = true;
        }
    }

    public void SetRoomPreset(GameObject anchor)
    {
        string name = anchor.name;
        if (name == "T")
        {
            rand = Random.Range(0, roomPresets.Ts.Length);
            Instantiate(roomPresets.Ts[rand], transform.position, roomPresets.Ts[rand].transform.rotation);
        }
        else if (name == "R")
        {
            rand = Random.Range(0, roomPresets.Rs.Length);
            Instantiate(roomPresets.Rs[rand], transform.position, roomPresets.Rs[rand].transform.rotation);
        }
        else if (name == "B")
        {
            rand = Random.Range(0, roomPresets.Bs.Length);
            Instantiate(roomPresets.Bs[rand], transform.position, roomPresets.Bs[rand].transform.rotation);
        }
        else if (name == "L")
        {
            rand = Random.Range(0, roomPresets.Ls.Length);
            Instantiate(roomPresets.Ls[rand], transform.position, roomPresets.Ls[rand].transform.rotation);
        }
        else if (name == "TR")
        {
            rand = Random.Range(0, roomPresets.TRs.Length);
            Instantiate(roomPresets.TRs[rand], transform.position, roomPresets.TRs[rand].transform.rotation);
        }
        else if (name == "TB")
        {
            rand = Random.Range(0, roomPresets.TBs.Length);
            Instantiate(roomPresets.TBs[rand], transform.position, roomPresets.TBs[rand].transform.rotation);
        }
        else if (name == "TL")
        {
            rand = Random.Range(0, roomPresets.TLs.Length);
            Instantiate(roomPresets.TLs[rand], transform.position, roomPresets.TLs[rand].transform.rotation);
        }
        else if (name == "RB")
        {
            rand = Random.Range(0, roomPresets.RBs.Length);
            Instantiate(roomPresets.RBs[rand], transform.position, roomPresets.RBs[rand].transform.rotation);
        }
        else if (name == "RL")
        {
            rand = Random.Range(0, roomPresets.RLs.Length);
            Instantiate(roomPresets.RLs[rand], transform.position, roomPresets.RLs[rand].transform.rotation);
        }
        else if (name == "BL")
        {
            rand = Random.Range(0, roomPresets.BLs.Length);
            Instantiate(roomPresets.BLs[rand], transform.position, roomPresets.BLs[rand].transform.rotation);
        }
        else if (name == "TRB")
        {
            rand = Random.Range(0, roomPresets.TRBs.Length);
            Instantiate(roomPresets.TRBs[rand], transform.position, roomPresets.TRBs[rand].transform.rotation);
        }
        else if (name == "TRL")
        {
            rand = Random.Range(0, roomPresets.TRLs.Length);
            Instantiate(roomPresets.TRLs[rand], transform.position, roomPresets.TRLs[rand].transform.rotation);
        }
        else if (name == "TBL")
        {
            rand = Random.Range(0, roomPresets.TBLs.Length);
            Instantiate(roomPresets.TBLs[rand], transform.position, roomPresets.TBLs[rand].transform.rotation);
        }
        else if (name == "RBL")
        {
            rand = Random.Range(0, roomPresets.RBLs.Length);
            Instantiate(roomPresets.RBLs[rand], transform.position, roomPresets.RBLs[rand].transform.rotation);
        }
        else if (name == "TRBL")
        {
            rand = Random.Range(0, roomPresets.TRBLs.Length);
            Instantiate(roomPresets.TRBLs[rand], transform.position, roomPresets.TRBLs[rand].transform.rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().isSpawned)
        {
            Destroy(gameObject);
        }
    }
}
