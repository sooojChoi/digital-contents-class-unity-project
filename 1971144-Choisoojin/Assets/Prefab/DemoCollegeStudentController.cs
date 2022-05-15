using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 20f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;
        private bool isKickboard = false;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Die();
                Attack();
                Jump();
                KickBoard();
                Run();
                EatHPItem();
            }
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isRun", false);
            // Debug.Log("물체와 충돌함");
            if (collision.gameObject.tag == "Monster")
            {
                Debug.Log("collisionEnter2D: 몬스터와 충돌함");
                Hurt();
                Managers.Data.PlayerData["hp"].content -= 20;
                // json 파일에 변경사항을 저장해준다. 
                playerInfoSave("/Resources/Data/playerData.json");
            }
        }
        void KickBoard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !isKickboard )
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }

        }

        void Run()
        {
            // 상인을 마주치면 일단 멈춘다.
            if(CameraMoving.meetingSellerNow == 1)
            {
                Vector3 moveVelocity = Vector3.zero;
                anim.SetBool("isRun", false);
                return;
            }

            if (!isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                anim.SetBool("isRun", false);


                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;

                    transform.localScale = new Vector3(direction, 1, 1);
                    if (!anim.GetBool("isJump"))
                        anim.SetBool("isRun", true);

                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;

                    transform.localScale = new Vector3(direction, 1, 1);
                    if (!anim.GetBool("isJump"))
                        anim.SetBool("isRun", true);

                }
                transform.position += moveVelocity * movePower * Time.deltaTime;

            }
            if (isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;

                    transform.localScale = new Vector3(direction, 1, 1);
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;

                    transform.localScale = new Vector3(direction, 1, 1);
                }
                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
        }
        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }
        void Hurt()
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
        void Die()
        {
            // 플레이어의 체력이 0이 된다면 죽는다.
            if (Managers.Data.PlayerData["hp"].content <= 0)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("die");
                alive = false;
            }
        }
        void EatHPItem()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // hp 아이템을 먹는다. (플레이어의 체력을 회복한다)
                playerData pd = Managers.Data.PlayerData["hpItem"];
                if (pd.sort != "" && Managers.Data.PlayerData["hp"].content < 1000)
                {
                    // 체력을 아이템의 hp만큼 증가시킨다.
                    Managers.Data.PlayerData["hp"].content += Managers.Data.ItemData[pd.sort].hp;

                    // 체력이 1000을 넘기면 1000으로 한다. (최대 1000을 넘기지 못하도록 한다.)
                    if (Managers.Data.PlayerData["hp"].content > 1000)
                    {
                        Managers.Data.PlayerData["hp"].content = 1000;
                    }

                    // 아이템을 먹었으니까 개수를 1만큼 줄인다.
                    Managers.Data.PlayerData[pd.sort].content -= 1;
                    // 만약 아이템을 다 먹었으면, PlayerData에서 제거한다.
                    if (Managers.Data.PlayerData[pd.sort].content == 0)
                    {
                        Managers.Data.PlayerData.Remove(pd.sort);  // 아이템 제거.
                        pd.sort = "";  // 설정된 hpItem이 없어짐.
                    }

                    // json 파일에 변경사항을 저장해준다. 
                    playerInfoSave("/Resources/Data/playerData.json");
                }
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }


        //  playerData.json 파일 저장하는 함수
        void playerInfoSave(string path)
        {
            List<playerData> playerInfo = new List<playerData>();
            playerDataInfo playerData = new playerDataInfo();

            foreach (KeyValuePair<string, playerData> player in Managers.Data.PlayerData)
            {
                playerInfo.Add(player.Value);
            }
            playerData.playerInfo = playerInfo;

            string jsonString = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.dataPath + path, jsonString);
        }

    }

}