using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DNAColumn : MonoBehaviour
{
    [SerializeField] List<float> healingCooldownLowToHighSpeeds = new List<float>();
    [SerializeField] private int remainingHPInDNA;
    public UnityEvent OnDNADeplete;
    private float healingCooldown;
    private float _timer; 
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Into contact with DNA column");
        _timer += Time.deltaTime;
        if (remainingHPInDNA > 30)
            healingCooldown = healingCooldownLowToHighSpeeds[2];
        else if (remainingHPInDNA <= 20 && remainingHPInDNA > 5)
            healingCooldown = healingCooldownLowToHighSpeeds[1];
        else
            healingCooldown = healingCooldownLowToHighSpeeds[0];

        if (_timer >= healingCooldown)
        {
            remainingHPInDNA -= 1;
            _timer = 0;
            Debug.Log($"Remaining HP: {remainingHPInDNA}");
        }
        if (remainingHPInDNA <= 0) {
            OnDNADeplete.Invoke();
            Destroy(gameObject);
        };
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
