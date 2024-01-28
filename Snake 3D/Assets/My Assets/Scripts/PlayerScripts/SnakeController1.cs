using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController1 : MonoBehaviour
{

    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 100;
    public int Gap = 10, BodysCountInStarting = 10;
    public float SteerChanger = 1;
    public bool SteerLock, UseNewSpawnPosition, UseTouchMove;
    public float bodyPartSpacing = 1.0f; // Adjust this value as needed for the desired spacing between body parts

    // References
    public GameObject BodyPrefab;
    public Transform BodySpawnPosition1;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    private Vector3 BodySpawnPosition2;
    [SerializeField]
    public float steerDirection, TouchDirection;

    // Start is called before the first frame update
    void Start()
    {

        //to increase Bodies in starting
        for (int I = 1; I <= BodysCountInStarting; I++)
        {
            GrowSnake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When Player On air He cant Steer ( This Condition Is Affected By Player_Gravity Script )
        if (!SteerLock)
        {

            // When TouchMove True Player Can steer with touch only (This Codition is affected by TouchController Script)
            if (!UseTouchMove)
            {
                steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1

            }

            if (UseTouchMove)
            {
                steerDirection = TouchDirection;  //(TouchController Script)
            }

            transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime * SteerChanger); // ( Affected by CollisionS Script ) SteerChanger is Used to Change The Steer Of The Player ex. 1 is for positive -1 is for negative
        }

    }

    private void FixedUpdate()
    {
        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.fixedDeltaTime;

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        for (int index = 0; index < BodyParts.Count; index++)
        {
            Vector3 point;

            // Calculate the target position for the body part based on spacing
            if (index == 0)
            {
                // The first body part follows the head closely
                point = transform.position + transform.forward * bodyPartSpacing;
            }
            else
            {
                // For the rest of the body parts, space them evenly along the snake's path
                int targetIndex = Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1);
                point = PositionsHistory[targetIndex];
            }

            // Move body towards the point along the snake's path
            Vector3 moveDirection = point - BodyParts[index].transform.position;
            BodyParts[index].transform.position += moveDirection * MoveSpeed * Time.fixedDeltaTime;

            // Rotate body towards the point along the snake's path
            BodyParts[index].transform.LookAt(point);

            BodySpawnPosition2 = point; // Where Body Spawn
        }
    }

    public void GrowSnake()
    {
        // Instantiate body instance and
        // add it to the list
        if (!UseNewSpawnPosition)
        {
            GameObject body = Instantiate(BodyPrefab, BodySpawnPosition1.position, Quaternion.identity); //This Will called at starting
            BodyParts.Add(body);
        }

        if (UseNewSpawnPosition)
        {
            GameObject body = Instantiate(BodyPrefab, BodySpawnPosition2, Quaternion.identity); //This will called when eating fruits
            BodyParts.Add(body);
        }

    }


}