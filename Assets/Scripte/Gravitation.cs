using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObjects;
    private Rigidbody rb;
    const float G = 0.006673f;

    public bool planet = false;
    public int orbitSpeed = 1000;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjects == null)  // ������ѵ���� class Gravitation �������
        { 
            otherObjects = new List<Gravitation>(); // ���ҧ List ���������� Gravitation
        }
        otherObjects.Add(this); // ����ѵ�ط���� Gravitation ����� List ��ª���

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed); 
        }
    }
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjects)
        {
            if (obj != this) // ����ҵ�ͧ������ѵ�ص��ͧ �����������Դ�ç�֧�ٴ���ͧ
            {
                AttractionForce(obj); // ���¡ Method ��������ç�֧�ٴ��ѧ�ӹǳ
            }
        }
    }
    void AttractionForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb; // �֧ Rigidbody �ͧ�ա�ѵ���������� m2
        Vector3 direction = rb.position - otherRb.position; // �ҷ�ȷҧ����ա�ѵ�������ȷҧ�˹

        float distance = direction.magnitude; // ��������ҧ�����ҧ�ѵ�بҡ Vector Direction ( ��� r )
        if (distance == 0f) return; // �ҡ�ѵ������㹵��˹觷���͹�ѹ ����ͧ���ç�֧�ٴ�ա

        // �ٵ��ç�֧�ٴ G = (m1 * m2) / r^2
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 gravitionalForce = forceMagnitude * direction.normalized; // ����ç��з�ȷҧ���͢�Ѻ�ѵ�ص���ç�֧�ٴ
        otherRb.AddForce(gravitionalForce); // ����ç�֧�ٴ���Ѻ�ѵ�����
    }
}
