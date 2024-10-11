using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSCreateCollider : MonoBehaviour
{
    public float force = 10;
    public float height = .5f;

    ParticleSystem ps;

    GameObject knockbackCollider;

    // Start is called before the first frame update

    [System.Obsolete]
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        knockbackCollider = transform.GetChild(0).gameObject;

        if (knockbackCollider != null)
        {
            //Destroy(this.gameObject, 1f);
            knockbackCollider.transform.localScale = new Vector3(ps.startSize, ps.startSize, ps.startSize);
        }

        StartCoroutine(StartCreateCollider());
    }

    [System.Obsolete]
    IEnumerator StartCreateCollider()
    {
        yield return new WaitForSeconds(ps.startDelay);

        if (knockbackCollider != null)
        {
            knockbackCollider.SetActive(true);

            yield return new WaitForSeconds(.1f);
            knockbackCollider.SetActive(false);
        }

        
    }
}
