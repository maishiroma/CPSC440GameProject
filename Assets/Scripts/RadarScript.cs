using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour {

    public Material SharedEnemyMaterial;
    public float iconSpawnDelay = 0.2f;
    public float killTime;
    public float RadarUpdateFrequency = .8f;
    public GameObject EnemyIconPrefab;

    private GameObject PlayerIcon;
    private Transform WorldSpaceRotationOffset;
    private Transform MainCamera;
    private float LastCameraYRot;
    private List<EnemyIcon> EnemyIcons = new List<EnemyIcon>();
    private Transform PlayerLocation;


	// Use this for initialization
	void Start ()
    {
        PlayerIcon = transform.FindChild("PlayerIcon").gameObject;
        WorldSpaceRotationOffset = transform.FindChild("WorldSpace");
        MainCamera = GameObject.Find("MainCamera").transform;
        StartCoroutine(UpdateIconLoop());
        PlayerLocation = GameObject.Find("PlayerSpawnLocation").transform;
	}
	

    public IEnumerator CreateEnemyIcon(GameObject SpawnedEnemy)
    {
        

        //Find Angle from Forward Direction
        Vector3 directionToEnemy = SpawnedEnemy.transform.position - PlayerLocation.position;
        directionToEnemy = new Vector3(directionToEnemy.x, 0, directionToEnemy.z);
        directionToEnemy.Normalize();

        float angleFromForward = Vector3.Angle(Vector3.forward, directionToEnemy);
        Vector3 Cross = Vector3.Cross(Vector3.forward, directionToEnemy);
        if(Cross.y > 0)
        {
            angleFromForward = -angleFromForward;
        }


        //Create Icon
        GameObject _icon = (GameObject)Instantiate(EnemyIconPrefab, WorldSpaceRotationOffset.position, WorldSpaceRotationOffset.rotation, WorldSpaceRotationOffset);
        _icon.transform.Rotate(angleFromForward, 0, 0);
        _icon.GetComponent<EnemyIcon>().lastAngleFromForward = angleFromForward;
        _icon.GetComponent<EnemyIcon>().Alien = SpawnedEnemy;
        EnemyIcons.Add(_icon.GetComponent<EnemyIcon>());
        _icon.SetActive(false);

        yield return new WaitForSeconds(iconSpawnDelay);
        _icon.SetActive(true);
    }

    public IEnumerator KillEnemyIcon(GameObject KilledEnemy)
    {


        yield return new WaitForSeconds(killTime);
    }


    void RestartUpdateIconLoop()
    {
        StartCoroutine(UpdateIconLoop());
    }

    IEnumerator UpdateIconLoop()
    {
        yield return new WaitForSeconds(RadarUpdateFrequency);

        List<EnemyIcon> enemyIconsToDelete = new List<EnemyIcon>();

        foreach(EnemyIcon enemyIcon in EnemyIcons)
        {
            if(enemyIcon.Alien != null)
            {
                enemyIcon.UpdateAlienIconPosition();

                

                //enemyIcon.transform.localRotation  = Quaternion.Euler(angleFromForward, enemyIcon.transform.localRotation.eulerAngles.y, enemyIcon.transform.localRotation.eulerAngles.z);


                //Find Angle from Forward Direction
                Vector3 directionToEnemy = enemyIcon.Alien.transform.position - PlayerLocation.position;
                directionToEnemy = new Vector3(directionToEnemy.x, 0, directionToEnemy.z);
                directionToEnemy.Normalize();

                float angleFromForward = Vector3.Angle(Vector3.forward, directionToEnemy);
                Vector3 Cross = Vector3.Cross(Vector3.forward, directionToEnemy);
                if (Cross.y > 0)
                {
                    angleFromForward = -angleFromForward;
                }


                if(Mathf.Abs(enemyIcon.lastAngleFromForward - angleFromForward) > .1f)
                {
                    enemyIcon.transform.localRotation = Quaternion.Euler(angleFromForward, 0,0);
                }


                enemyIcon.lastAngleFromForward = angleFromForward;

                



            }
            else
            {
                enemyIconsToDelete.Add(enemyIcon);
            }
        }

        foreach(EnemyIcon enemyIcon in enemyIconsToDelete)
        {
            EnemyIcons.Remove(enemyIcon);
            Destroy(enemyIcon.gameObject);
        }
        
        RestartUpdateIconLoop();
    }

    

	// Update is called once per frame
	void Update ()
    {



        //Update WorldSpace rotation to account for camera rotation between updates
        WorldSpaceRotationOffset.Rotate(MainCamera.eulerAngles.y - LastCameraYRot,0,0);
        LastCameraYRot = MainCamera.eulerAngles.y;
    }
}
