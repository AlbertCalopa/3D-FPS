using UnityEngine;
using System.Collections.Generic;

public class TCObjectPool
{
    List<GameObject> m_ObjectsPool;
    int m_CurrentElementId = 0;
    public TCObjectPool(int ElementCount, GameObject Element)
    {
        m_ObjectsPool = new List<GameObject>();
        for(int i = 0; i< ElementCount; i++)
        {
            GameObject l_Element = GameObject.Instantiate(Element);
            l_Element.SetActive(true);
            m_ObjectsPool.Add(l_Element);
        }
        m_CurrentElementId = 0;
    }
    public GameObject GetNextElement()
    {
        GameObject l_Element = m_ObjectsPool[m_CurrentElementId];
        ++m_CurrentElementId;
        if(m_CurrentElementId >= m_ObjectsPool.Count)
        {
            m_CurrentElementId = 0;
        }
        return l_Element;
    }
}
