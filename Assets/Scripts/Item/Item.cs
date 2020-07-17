using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Item
{
    class Item : MonoBehaviour
    {
        public float m_AddingHealth = 20f;    //luong mau tang them khi an item

        public float m_AddingSpeedTime = 10f; //thoi gian tang van toc khi an item

        public float m_NewSpeed = 17f;     //van toc di chuyen khi an item

        public float m_OldSpeed = 12f;     //van toc ban dau

        public IEnumerator waitAndRun(Collider other)
        {
            // WAIT FOR 3 SEC
            yield return new WaitForSeconds(3);
            // RUN YOUR CODE HERE ...
            other.gameObject.GetComponent<TankMovement>().m_Speed = m_OldSpeed;

        }

        IEnumerator ExampleCoroutine()
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(5);

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }

        private IEnumerator OnTriggerEnter(Collider other)
        {
            // Find all the tanks in an area around the shell and damage them.
            if (other.gameObject.tag == "Player")
            {
                if (gameObject.tag == "Heart")
                {
                    other.gameObject.GetComponent<TankHealth>().AddHealth(m_AddingHealth);
                    Vector3 moveOut = new Vector3(transform.position.x, transform.position.y - 100f, transform.position.z);
                    transform.position = moveOut;
                }
                else {
                    other.gameObject.GetComponent<TankMovement>().m_Speed = m_NewSpeed;
                    Vector3 moveOut = new Vector3(transform.position.x, transform.position.y - 100f, transform.position.z);
                    transform.position = moveOut;
                    yield return other.gameObject.GetComponent<TankMovement>().resetSpeed(m_OldSpeed);
                }

                
                //dat lai vi tri spawnItem = 0 de tiep tuc spawn
                GameObject[] gameobjs = GameObject.FindGameObjectsWithTag("GameManager");
                Transform[] m_SpawnPointItem = gameobjs[0].GetComponent<GameManager>().m_SpawnPointItem;
                Vector3 oldPosition = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);

                for (int i = 0; i < m_SpawnPointItem.Length; i++) {
                    if (oldPosition == m_SpawnPointItem[i].position){
                        GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>().itemSpawn[i] = 0;
                    }
                    //Vector3 toTarget = m_SpawnPointItem[i].position - transform.position;
                    //float distance = toTarget.magnitude;
                    //if (distance<101) {
                    //    GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>().itemSpawn[i] = 0;
                    //}
                }
                Destroy(gameObject);
                
            }
        }
    }
}
