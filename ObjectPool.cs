using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectType
{
    public Component objs;      // ������ ����� ������Ʈ 
    public int StartCount;      // ���۽� ������ ������Ʈ ����
    public Queue<Component> objList = new Queue<Component>();
}


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool objectpool;
    [SerializeField] ObjectType[] bulletType;
    Queue<Component>[] list;


    void Awake()
    {
        objectpool = this;
        list = new Queue<Component>[bulletType.Length];
    }

    void Start()
    {

        for (int i = 0; i < bulletType.Length; i++)
        {
            list[i] = bulletType[i].objList;

            for(int j = 0; j < bulletType[i].StartCount; j++)
            {
                GameObject target = Instantiate(bulletType[i].objs.gameObject);
                list[i].Enqueue(target.GetComponent(bulletType[i].objs.GetType()));
                target.SetActive(false);
            }

        }

    }

    public GameObject GetObject(int Type)   // ������Ʈ ������ ���
    {
        GameObject target = null;

        for (int i = 0; i < list[Type].Count ; i++)
        {
            GameObject objs = list[Type].Dequeue().gameObject;
            list[Type].Enqueue(objs.GetComponent(bulletType[Type].objs.GetType()));

            if (!objs.gameObject.activeSelf)
            {
                target = objs.gameObject;
                break;
            }

           
        }

        if (target == null)
        {
            target = Instantiate(bulletType[Type].objs.gameObject);
            list[Type].Enqueue(target.GetComponent(bulletType[Type].objs.GetType()));
        }

        target.SetActive(true);
        return target;
    }


    public void DestoryObject(Component destoryObj) // ������Ʈ ������ ���
    {
        destoryObj.gameObject.SetActive(false);
    }

    

}
