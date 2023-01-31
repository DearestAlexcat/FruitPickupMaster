using UnityEngine;
using System.Collections.Generic;

public class Pooler<T> where T : Component
{
	protected Stack<T> m_FreeInstances = new Stack<T>();
	protected T m_Original;
	protected Transform m_parent;

	public Pooler(T original, int initialSize, Transform parent = null)
	{
		m_Original = original;
		m_FreeInstances = new Stack<T>(initialSize);
		m_parent = parent;

		for (int i = 0; i < initialSize; ++i)
		{
			T obj = Object.Instantiate(m_Original, m_parent);
			obj.gameObject.SetActive(false);
			m_FreeInstances.Push(obj);
		}
	}

	public T Get()
	{
		return Get(Vector3.zero, Quaternion.identity);
	}

	public T Get(Vector3 pos, Quaternion quat)
	{
	    T ret = m_FreeInstances.Count > 0 ? m_FreeInstances.Pop() : Object.Instantiate(m_Original, m_parent);

		ret.gameObject.SetActive(true);
		ret.gameObject.transform.position = pos;
		ret.gameObject.transform.rotation = quat;

		return ret;
	}

	public void Clear()
    {
		int count = m_FreeInstances.Count;

		for (int i = 0; i < count; i++)
        {
			Object.Destroy(m_FreeInstances.Pop().gameObject);
		}
    }

	public void Free(T obj)
	{
		//obj.gameObject.transform.SetParent(null);
		obj.gameObject.SetActive(false);
		m_FreeInstances.Push(obj);
	}
}
