using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SortController : MonoBehaviour
{
    [SerializeField]  List<DataSort> lsDataSort;
    [SerializeField] List<ParentObjectInGame > lsObjectInGame;
    public DataSort GetData
    {
        get
        {
           foreach(var data in lsDataSort)
           {
            if(data.wasFull == false)
            {
                return data;
            }
           }
           return null;
        }
    }
    public List<CountIdObj> lsCountIdObj;

    public CountIdObj GetCountIdObj(int id)
    {
         foreach(var countIdObj in lsCountIdObj)
         {
            if(countIdObj.id == id)
            {
                return countIdObj;
            }
         }
         return null;
    }
    public ParentObjectInGame pfbParentObjectInGame ;

    public void Init()
    {
     
lsObjectInGame = new List<ParentObjectInGame>();


    }
    

    public void HandleActionClick( ObjectInGame objectInGameParam)
    {

         var parentObjectInGame = Instantiate(pfbParentObjectInGame);
         parentObjectInGame.id = objectInGameParam.id;
      //   objectInGameParam.transform.SetParent(parentObjectInGame.transform);
              var post = GetData;
              if(post != null)
              {  
                if(lsObjectInGame.Count <= 0)
                {
                 
                   post.objectInGame = objectInGameParam;
                           objectInGameParam.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                           objectInGameParam.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
                           lsObjectInGame.Add(parentObjectInGame);
                     Debug.Log("Neu chua co doi tuong nao trong list");
                 }
                else 
                {
                   int index = -1;
                   int indexInsert = -1;
                    for(int i = 0 ; i < lsObjectInGame.Count ; i++)
                    {
                        if(lsObjectInGame[i].id == objectInGameParam.id)
                        {
                           if(i == lsObjectInGame.Count - 1)
                           {
                                   index = 1; //add vao cuoi
                                   Debug.Log("giong id cuoi");   
                           }
                           else 
                           {
                                index = 2;  // insert vao giua
                                indexInsert = i;
                                  Debug.Log("giong id giua");  
                                  break;   
                           }
                        }
                        else 
                        {
                              index = 1; //khong co id nao giong
                           Debug.Log(" k giong id nao");  
                          
                        }

                    }

                if(index == 1)
                {
                   
                     post.objectInGame = objectInGameParam;
                           objectInGameParam.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                           objectInGameParam.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
                           lsObjectInGame.Add(parentObjectInGame);
                }
                else if(index == 2)
                {
                      
                      post.objectInGame = objectInGameParam;
                    objectInGameParam.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    objectInGameParam.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
                    lsObjectInGame.Insert(indexInsert + 1, parentObjectInGame);
                }


                }
              
              
               HandleMove(  parentObjectInGame, objectInGameParam);
    }
    
    }


    public void HandleMove( ParentObjectInGame    objectInGameParam, ObjectInGame objectInGameChild)
    {

        for (int i = 0; i < lsObjectInGame.Count ; i++)
        {
           int index = i;
           lsObjectInGame[i].transform.position = lsDataSort[i].postData.transform.position;
           
        }
          

        objectInGameChild.transform.SetParent(objectInGameParam.transform);
        objectInGameChild.transform.DOLocalMove(Vector3.zero , 0.1f).OnComplete(() => {
           
           var temp = GetCountIdObj(objectInGameParam.id);
                if(temp != null)
                {
                       temp.lsObjectInGame.Add(objectInGameParam);
                        if(temp.lsObjectInGame.Count == 3)
                        {
                             foreach(var objectInGame in temp.lsObjectInGame)
                           {
                                if(lsObjectInGame.Contains(objectInGame))
                                {
                                    lsObjectInGame.Remove(objectInGame);
                                }
                           }
                           foreach(var objectInGame in temp.lsObjectInGame)
                           {
                              Destroy(objectInGame.gameObject);
                           }
                           temp.lsObjectInGame.Clear();
                        }

                }
                MoveAfterDestroy();
        });   


    }

    

    private void MoveAfterDestroy()
    {
           for (int i = 0; i < lsObjectInGame.Count ; i++)
        {
           int index = i;
            lsObjectInGame[i].transform.position = lsDataSort[i].postData.transform.position;

        }

    
        
    }
    




}
[System.Serializable]
public class DataSort 
{
    public int id;
   public PostInGamePlay postData;
   public ObjectInGame objectInGame;
   public bool wasFull
   {
    get{
        if(objectInGame != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   }
}
[System.Serializable]
public class CountIdObj
{
    public int id;
    public List<ParentObjectInGame  > lsObjectInGame;
}

