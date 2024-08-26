using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFx : MonoBehaviour
{
    //called every time when a game object is activated
    void OnEnable()
    {
        Invoke("DeactivateGameObject",2f); //activate a function after 2 seconds
        
    }
    /*eğer OnEnable yerine Start function'da kullansaydık: Mesela 100 tane bullet 
    oluşturup ateşlendikçe visible yapıyoruz. Sonra deactivate ile tekrardan invisible
    yapıyoruz. Ama start function'da kullandığımızda mesela 1. buşşet ateşlenip siliniyo.
    Sonra 2. bulet ateşlenip siliniyo ama sonra ilk invisible olan bullet tekrar
    1. spawn olunca, 1. bullet tekrar respawn'lanıyo. Ama onenable sadece bir object
    aktifleştirilince devreye giriyor ve önceden deaktifleştirdiğimiz bulletları
    visible yapmıyor
    */

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }


} //class




























