//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
//{
//    private static T instance;

//    public static T Instance
//    {
//        get
//        {
//            //인스턴스가 null이면
//            if (instance == null)
//            {
//                //만약 씬에 이미 해당타입의 인스턴스가 있으면
//                instance = (T)FindObjectOfType(typeof(T));

//                if (instance == null)
//                {
//                    GameObject singletonObject = new GameObject();
//                    instance = singletonObject.AddComponent<T>();

//                    DontDestroyOnLoad(singletonObject);//씬이 변경되더라도 해당오브젝트를 없애지않음
//                }
//            }

//            return instance;
//        }
//    }
//}
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null) // 만약 인스턴스가 null이라면
            {
                instance = (T)FindObjectOfType(typeof(T)); // 어딘가에 인스턴스가 있을 수 있으니 인스턴스를 FindObejctofType으로 탐색

                if (instance == null) // 없다면, 게임 오브젝트를 생성
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // DontSestroyOnLoad가 다른 오브젝트의 하위에 있다면 작동 X, 매니저가 부모이거나, 자식일 때 작동
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // 씬이 전환되도 오브젝트가 파괴되지 않는다.
        }
    }
}