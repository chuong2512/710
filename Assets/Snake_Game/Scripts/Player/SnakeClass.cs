using System.Collections;
using UnityEngine;

public static class SnakeSelectionCheck
{
    public static bool snake1Selected, snake2Selected, snake3Selected ,snake4Selected ,snake5Selected = false;
}
public abstract class SnakeClass : MonoBehaviour,IPlayerBehaviour
{
    [Header("Snake Nodes")]
    [Tooltip("Drag and Drop the nodes here")]
    public Transform[] nodes;

    [Header("Grid Detection Dist from Snake Head")]
    [Tooltip("Raycast distance from Snake Head to the nearest Grids ")]
    public float rayDistance;

    [Header("Grid Selection")]
    [Tooltip("Select only the grid Layer ")]
    public LayerMask GridLayer;
    GameObject GridHolder;
    protected GameObject[] Grids;

    [Header("Snake Body")]
    [Tooltip("Assign the Snake Head")]
    public GameObject SnakeHead;
    [Tooltip("Assign all the SnakeBodies from head to bottom order")]
    public Transform[] SnakeBody;
    bool isMoving = false;
    float speed = 15;
    

    [Header("Cursor Controller")]
    [Tooltip("Assign the cursor GameObject")]
    public GameObject Cursor;
    float cursorSpeed = 50;
    protected Vector3 targetPos;
    protected bool isSnakeFinished = false;

    [Header("Snake Exit")]
    [Tooltip("Assign the Exit Path GameObject")]
    GameObject exitPathHolder;
    GameObject /*WayPointL1,*/ WayPointR1, /*EndPointL1,*/ EndPointR1;
    protected bool isLeftExit, isRightExit = false;
    protected bool isTurnRightL1, isTurnRightR1 = false;
    
    Ray ray;
    protected bool isRayEnabled = false;
    int layer = 1 << 16;

    Animator anim;
    bool isanimation = false;

    //private Transform CurrentBodyPart;
    //private Transform previousBodyPart;
    private void Start()
    {
        // References
        exitPathHolder = GameObject.FindWithTag("ExitPath");
        GridHolder = GameObject.FindWithTag("AllGrids");
        WayPointR1 = exitPathHolder.transform.Find("WayPointR1").transform.gameObject;
        EndPointR1 = exitPathHolder.transform.Find("EndPointR1").transform.gameObject;
        anim = gameObject.transform.parent.transform.parent.GetComponent<Animator>();

        //Other
        GameManager.instance.IntitializeScore();
        Cursor.gameObject.SetActive(true);

        //Grid Detection
        Grids = new GameObject[GridHolder.transform.childCount];
        for (int i = 0; i < GridHolder.transform.childCount; i++)
        {
            Grids[i] = GridHolder.transform.GetChild(i).gameObject;
            Grids[i].gameObject.GetComponent<CanMove>().isOccupied = false;
        }
    }
   
    public virtual void LateUpdate()
    {
        if (!UImanager.instance.isPaused)
        {
            SnakeExit();
            GridBooleans();
            RaycastCheck();
            NearestGrid();
            VictoryCheck();
            SnakeSmoothMovement();
        }
    }

    private void OnMouseDrag()
    {
        if (!UImanager.instance.isPaused)
        {
            Move();
            HandHelper();
            BorderHit();
        }
    }

    public virtual void HandHelper()
    {
        switch (UImanager.instance.sceneIndex)
        {
            case 1:
                HandTut.instance.handHelperDisable();
                break;
            case 2:
                HandTut.instance.handHelperDisable();
                break;
            case 3:
                StartCoroutine(DisableHand(.4f));
                break;
        }
    }

    IEnumerator DisableHand(float time)
    {
        yield return new WaitForSeconds(time);
        HandTut.instance.handHelper2Disable();
        HandTut.instance.handHelperDisable();
    }

    private void OnMouseDown()
    {
        isRayEnabled = true;
        if (!UImanager.instance.isPaused)
        {
            SnakeSelection();
        }
    }

    private void OnMouseUp()
    {
        isRayEnabled = false;
    }
    
    private void BorderHit()
    {
        if(isRayEnabled)
        {
            int layerborder = 1 << 17;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerborder))
            {
                    AnimationOn();
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
    //Should Override
    public virtual void SnakeSelection()
    {
        if (isRayEnabled)
        {
            SnakeSelectionCheck.snake1Selected = true;
            SnakeSelectionCheck.snake2Selected = SnakeSelectionCheck.snake3Selected = SnakeSelectionCheck.snake4Selected
                = SnakeSelectionCheck.snake5Selected = false;
        }
    }

    public virtual void NearestGrid()
    {
        //if (isRayEnabled)
        //{
        if (!UImanager.instance.isPaused)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                MovetoNearestGrid(hit);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        //}
    }
    //Should Override
    public virtual void MovetoNearestGrid(RaycastHit hit)
    {
        if (hit.collider != null && SnakeSelectionCheck.snake1Selected)
        {
            if (hit.collider.gameObject.GetComponent<CanMove>().player1CanMoveToThisTile &&
                        !hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                SnakeBehaviour(hit);
            }
        }
    }

    public void Move()
    {
        if (isRayEnabled)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layer))
            {
                if (hit.collider != null)
                {
                    GettingGridProperties(hit);
                }
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
    
    // Should override
    public virtual void GettingGridProperties( RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Grid"))
        {
            if (hit.collider.gameObject.GetComponent<CanMove>().player1CanMoveToThisTile &&
                        !hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                SnakeBehaviour(hit);
            }
            if(hit.collider.gameObject.GetComponent<CanMove>().player1CanMoveToThisTile && hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                AnimationOn();
            }
        }
    }
    //should override
   
    public void AnimationOn()
    {
        isanimation = true;
        if (isanimation)
        {
            anim.SetBool("Hit", true);
            StartCoroutine(AnimationStop(.2f));
        }
    }

    public IEnumerator AnimationStop(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("Hit", false);
        isanimation = false;
       
    }

    public void SnakeBehaviour(RaycastHit hit)
    {
        isMoving = true;
        NodeMovement();
        SnakeBody[0].transform.position = SnakeHead.transform.position;
        Vector3 headPos = transform.position;
        SnakeHead.transform.position = headPos;
        targetPos = hit.collider.gameObject.transform.position;
        transform.position = targetPos;
        AudioManager.instance.Move();
    }

    public void NodeMovement()
    {
        //for (int i = 1; i < SnakeBody.Length; i++)
        //{
        //    if (SnakeBody.Length > i)
        //    {
        //        SnakeBody[i].transform.position = SnakeBody[i - 1].transform.position;

        //    }
        //}
        #region SnakeBodyExtension Direct Method
        //if (SnakeBody.Length > 20) SnakeBody[20].transform.position = SnakeBody[19].transform.position;

        //if (SnakeBody.Length > 19) SnakeBody[19].transform.position = SnakeBody[18].transform.position;

        //if (SnakeBody.Length > 18) SnakeBody[18].transform.position = SnakeBody[17].transform.position;

        //if (SnakeBody.Length > 17) SnakeBody[17].transform.position = SnakeBody[16].transform.position;

        //if (SnakeBody.Length > 16) SnakeBody[16].transform.position = SnakeBody[15].transform.position;

        if (SnakeBody.Length > 15) SnakeBody[15].transform.position = SnakeBody[14].transform.position;

        if (SnakeBody.Length > 14) SnakeBody[14].transform.position = SnakeBody[13].transform.position;

        if (SnakeBody.Length > 13) SnakeBody[13].transform.position = SnakeBody[12].transform.position;

        if (SnakeBody.Length > 12) SnakeBody[12].transform.position = SnakeBody[11].transform.position;

        if (SnakeBody.Length > 11) SnakeBody[11].transform.position = SnakeBody[10].transform.position;

        if (SnakeBody.Length > 10) SnakeBody[10].transform.position = SnakeBody[9].transform.position;

        if (SnakeBody.Length > 9) SnakeBody[9].transform.position = SnakeBody[8].transform.position;

        if (SnakeBody.Length > 8) SnakeBody[8].transform.position = SnakeBody[7].transform.position;

        if (SnakeBody.Length > 7) SnakeBody[7].transform.position = SnakeBody[6].transform.position;

        if (SnakeBody.Length > 6) SnakeBody[6].transform.position = SnakeBody[5].transform.position;

        if (SnakeBody.Length > 5) SnakeBody[5].transform.position = SnakeBody[4].transform.position;

        if (SnakeBody.Length > 4) SnakeBody[4].transform.position = SnakeBody[3].transform.position;

        if (SnakeBody.Length > 3) SnakeBody[3].transform.position = SnakeBody[2].transform.position;

        if (SnakeBody.Length > 2) SnakeBody[2].transform.position = SnakeBody[1].transform.position;

        if (SnakeBody.Length > 1) SnakeBody[1].transform.position = SnakeBody[0].transform.position;

        #endregion
    }
    public void SnakeSmoothMovement()
    {
        if(isMoving)
        {
            Cursor.transform.position = Vector3.Lerp(Cursor.transform.position, transform.position - new Vector3(0,0,.4f), cursorSpeed * Time.deltaTime);
            nodes[0].transform.position = Vector3.Lerp(nodes[0].transform.position, transform.position, speed * Time.deltaTime);
            nodes[1].transform.position = Vector3.Lerp(nodes[1].transform.position, SnakeHead.transform.position, speed * Time.deltaTime);
            #region NodesExtension Direct Method
            if (nodes.Length > 2)
                nodes[2].transform.position = Vector3.Lerp(nodes[2].transform.position, SnakeBody[0].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 3)
                nodes[3].transform.position = Vector3.Lerp(nodes[3].transform.position, SnakeBody[1].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 4)
                nodes[4].transform.position = Vector3.Lerp(nodes[4].transform.position, SnakeBody[2].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 5)
                nodes[5].transform.position = Vector3.Lerp(nodes[5].transform.position, SnakeBody[3].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 6)
                nodes[6].transform.position = Vector3.Lerp(nodes[6].transform.position, SnakeBody[4].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 7)
                nodes[7].transform.position = Vector3.Lerp(nodes[7].transform.position, SnakeBody[5].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 8)
                nodes[8].transform.position = Vector3.Lerp(nodes[8].transform.position, SnakeBody[6].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 9)
                nodes[9].transform.position = Vector3.Lerp(nodes[9].transform.position, SnakeBody[7].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 10)
                nodes[10].transform.position = Vector3.Lerp(nodes[10].transform.position, SnakeBody[8].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 11)
                nodes[11].transform.position = Vector3.Lerp(nodes[11].transform.position, SnakeBody[9].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 12)
                nodes[12].transform.position = Vector3.Lerp(nodes[12].transform.position, SnakeBody[10].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 13)
                nodes[13].transform.position = Vector3.Lerp(nodes[13].transform.position, SnakeBody[11].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 14)
                nodes[14].transform.position = Vector3.Lerp(nodes[14].transform.position, SnakeBody[12].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 15)
                nodes[15].transform.position = Vector3.Lerp(nodes[15].transform.position, SnakeBody[13].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 16)
                nodes[16].transform.position = Vector3.Lerp(nodes[16].transform.position, SnakeBody[14].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 17)
                nodes[17].transform.position = Vector3.Lerp(nodes[17].transform.position, SnakeBody[15].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 18)
                nodes[18].transform.position = Vector3.Lerp(nodes[18].transform.position, SnakeBody[16].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 19)
                nodes[19].transform.position = Vector3.Lerp(nodes[19].transform.position, SnakeBody[17].transform.position, speed * Time.deltaTime);
            if (nodes.Length > 20)
                nodes[20].transform.position = Vector3.Lerp(nodes[20].transform.position, SnakeBody[18].transform.position, speed * Time.deltaTime);
            #endregion 
        }
    }
    //Should Override
    public virtual void GridBooleans()
    {
        foreach (GameObject grid in Grids)
        {
            grid.GetComponent<CanMove>().player1CanMoveToThisTile = false;
        }
    }
   
    public void RaycastCheck()
    {
        #region RayCast to check Right Grid
        // Right
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        RaycastHit hitRight;

        if (Physics.Raycast(rayRight, out hitRight, rayDistance, GridLayer))
        {
            HitCheck(hitRight);
            //transform.position = Vector3.Slerp(transform.position, hitRight.collider.transform.position, 1f);
            Debug.DrawLine(rayRight.origin, hitRight.point, Color.red);

        }
        else
        {
            Debug.DrawLine(rayRight.origin, rayRight.origin + rayRight.direction * rayDistance, Color.blue);
        }
        #endregion

        #region RayCast to check Left Grid
        // Left
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        RaycastHit hitLeft;
        if (Physics.Raycast(rayLeft, out hitLeft, rayDistance, GridLayer))
        {
            if (hitLeft.collider != null)
            {
                HitCheck(hitLeft);
                //transform.position = hitLeft.collider.transform.position;
                Debug.DrawLine(rayLeft.origin, hitLeft.point, Color.red);
            }
        }
        else
        {
            Debug.DrawLine(rayLeft.origin, rayLeft.origin + rayLeft.direction * rayDistance, Color.blue);
        }
        #endregion

        #region RayCast to check Upper Grid
        // Up
        Ray rayUp = new Ray(transform.position, transform.TransformDirection(Vector3.up));
        RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, rayDistance, GridLayer))
        {
            HitCheck(hitUp);
            //transform.position = hitUp.collider.transform.position;
            Debug.DrawLine(rayUp.origin, hitUp.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayUp.origin, rayUp.origin + rayUp.direction * rayDistance, Color.blue);
        }
        #endregion

        #region RayCast to check Down Grid
        // Down
        Ray rayDown = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        RaycastHit hitDown;
        if (Physics.Raycast(rayDown, out hitDown, rayDistance, GridLayer))
        {
            HitCheck(hitDown);
            //transform.position = hitDown.collider.transform.position;
            Debug.DrawLine(rayDown.origin, hitDown.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayDistance, Color.blue);
        }
        #endregion
    }

    //public  void RotationChecks(RaycastHit hit)
    //{
    //    if (hit.collider.transform.position.x > transform.position.x && hit.collider.transform.position.y == transform.position.y)
    //    {
    //        //isRight = true;
    //        //isLeft = isUp = isDown = false;
    //        //transform.eulerAngles = new Vector3(0, 0, 0);
    //        //Debug.Log("MovingRight");
    //    }
    //    if (hit.collider.transform.position.x < transform.position.x && hit.collider.transform.position.y == transform.position.y)
    //    {
    //        //isLeft = true;
    //        //isRight = isUp = isDown = false;
    //        //transform.eulerAngles = new Vector3(0, 0, 180);
    //        //Debug.Log("MovingLeft");
    //    }
    //    if (/*hit.collider.transform.position.x == transform.position.x && */hit.collider.transform.position.y > transform.position.y)
    //    {
    //        //isUp = true;
    //        //isRight = isLeft = isDown = false;
    //        //transform.eulerAngles = new Vector3(0, 0, 90);
    //        //Debug.Log("MovingUp");
    //    }
    //    if (/*hit.collider.transform.position.x == transform.position.x && */hit.collider.transform.position.y < transform.position.y)
    //    {
    //        //isDown = true;
    //        //isRight = isLeft = isUp = false;
    //        //transform.eulerAngles = new Vector3(0, 0, -90);
    //        //Debug.Log("MovingDown");
    //    }
    //}


    //Should override
    public virtual void HitCheck(RaycastHit hit)
    {
        if(!isLeftExit && !isSnakeFinished)
            hit.collider.gameObject.GetComponent<CanMove>().player1CanMoveToThisTile = true;
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MoverL"))
        {
            GameManager.instance.AddScorePoints();
            GameManager.instance.CheckForVictory();
            Cursor.SetActive(false);
            isLeftExit = true;
            isSnakeFinished = true;
        }
        if (other.gameObject.CompareTag("Mover"))
        {
            AudioManager.instance.Exit();
            GameManager.instance.AddScorePoints();
            GameManager.instance.CheckForVictory();
            Cursor.SetActive(false);
            isRightExit = true;
            isSnakeFinished = true;
        }
        if (other.gameObject.CompareTag("RightTurn"))
        {
            isTurnRightL1 = true;
        }
        if (other.gameObject.CompareTag("RightTurnR1"))
        {
            isTurnRightR1 = true;
        }
        if (other.gameObject.CompareTag("SnakeDisable"))
        {
            StartCoroutine(DisableSnake(2f));
        }
        

    }
    
    public virtual void SnakeExit()
    {
        //if (isLeftExit && isSnakeFinished)
        //{
        //    MovetoWayPointL1(11f);
        //    SmoothMovement(10f);
            
        //}
        if(isRightExit && isSnakeFinished)
        {
            MovetoWayPointR1(10.5f);
            SmoothMovement(10f);
        }
        //if (isTurnRightL1)
        //{
        //    isLeftExit = false;
        //    MovetoEndPointL1(11f);
        //    SmoothMovement(10f);
        //}
        if(isTurnRightR1)
        {
            isRightExit = false;
            MovetoEndPointR1(10.5f);
            SmoothMovement(10f);
        }

    }

    public virtual void SmoothMovement(float time)
    {
        Vector3 headPos = transform.position;
        SnakeHead.transform.position = Vector3.MoveTowards(SnakeHead.transform.position, headPos, time * Time.deltaTime);
        SnakeBody[0].transform.position = Vector3.MoveTowards(SnakeBody[0].transform.position, SnakeHead.transform.position, time * Time.deltaTime);
        if(SnakeBody.Length > 1)
            SnakeBody[1].transform.position = Vector3.MoveTowards(SnakeBody[1].transform.position, SnakeBody[0].transform.position, time * Time.deltaTime);
        if(SnakeBody.Length > 2)
            SnakeBody[2].transform.position = Vector3.MoveTowards(SnakeBody[2].transform.position, SnakeBody[1].transform.position, time * Time.deltaTime);
        if(SnakeBody.Length >3)
            SnakeBody[3].transform.position = Vector3.MoveTowards(SnakeBody[3].transform.position, SnakeBody[2].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 4)
            SnakeBody[4].transform.position = Vector3.MoveTowards(SnakeBody[4].transform.position, SnakeBody[3].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 5)
            SnakeBody[5].transform.position = Vector3.MoveTowards(SnakeBody[5].transform.position, SnakeBody[4].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 6)
            SnakeBody[6].transform.position = Vector3.MoveTowards(SnakeBody[6].transform.position, SnakeBody[5].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 7)
            SnakeBody[7].transform.position = Vector3.MoveTowards(SnakeBody[7].transform.position, SnakeBody[6].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 8)
            SnakeBody[8].transform.position = Vector3.MoveTowards(SnakeBody[8].transform.position, SnakeBody[7].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 9)
            SnakeBody[9].transform.position = Vector3.MoveTowards(SnakeBody[9].transform.position, SnakeBody[8].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 10)
            SnakeBody[10].transform.position = Vector3.MoveTowards(SnakeBody[10].transform.position, SnakeBody[9].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 11)
            SnakeBody[11].transform.position = Vector3.MoveTowards(SnakeBody[11].transform.position, SnakeBody[10].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 12)
            SnakeBody[12].transform.position = Vector3.MoveTowards(SnakeBody[12].transform.position, SnakeBody[11].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 13)
            SnakeBody[13].transform.position = Vector3.MoveTowards(SnakeBody[13].transform.position, SnakeBody[12].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 14)
            SnakeBody[14].transform.position = Vector3.MoveTowards(SnakeBody[14].transform.position, SnakeBody[13].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 15)
            SnakeBody[15].transform.position = Vector3.MoveTowards(SnakeBody[15].transform.position, SnakeBody[14].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 16)
            SnakeBody[16].transform.position = Vector3.MoveTowards(SnakeBody[16].transform.position, SnakeBody[15].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 17)
            SnakeBody[17].transform.position = Vector3.MoveTowards(SnakeBody[17].transform.position, SnakeBody[16].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 18)
            SnakeBody[18].transform.position = Vector3.MoveTowards(SnakeBody[18].transform.position, SnakeBody[17].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 19)
            SnakeBody[19].transform.position = Vector3.MoveTowards(SnakeBody[19].transform.position, SnakeBody[18].transform.position, time * Time.deltaTime);
        if (SnakeBody.Length > 20)
            SnakeBody[20].transform.position = Vector3.MoveTowards(SnakeBody[20].transform.position, SnakeBody[19].transform.position, time * Time.deltaTime);

    }

    //public virtual void MovetoWayPointL1(float time) 
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, WayPointL1.transform.position, time * Time.deltaTime);
    //}

    public virtual void MovetoWayPointR1(float time) 
    {
        transform.position = Vector3.MoveTowards(transform.position, WayPointR1.transform.position, time * Time.deltaTime);
    }

    //public virtual void MovetoEndPointL1(float time) 
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, EndPointL1.transform.position, time * Time.deltaTime);
    //}

    public virtual void MovetoEndPointR1(float time) 
    {
        transform.position = Vector3.MoveTowards(transform.position, EndPointR1.transform.position, time * Time.deltaTime);
    }
    
    IEnumerator DisableSnake(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void VictoryCheck()
    {
        if (GameManager.instance.isVictory)
        {
            StartCoroutine(victoryPanel(.35f));
            UImanager.instance.LevelVictory();
            GameManager.instance.isVictory = false;
        }
    }

    IEnumerator victoryPanel(float time)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.Victory();
        ParticleEffects.instance.ConfettiEnable();
    }


}
