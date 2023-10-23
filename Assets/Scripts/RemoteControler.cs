using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoteControler : MonoBehaviour
{
    BluetoothManagement bluetooth;

    bool isTurning = false;

    void Awake()
    {
        bluetooth = BluetoothManagement.Instance;
    }

    public void Move(int dir)
    {        
        bluetooth.Send(dir);
    }

    public void Turn()
    {
        if (!isTurning)
        {
            StartCoroutine(Turning());
        }

    }

    IEnumerator Turning()
    {
        isTurning = true;

        bluetooth.Send(Direction.Forward);

        yield return new WaitForSeconds(1f);

        bluetooth.Send(Direction.Left);

        yield return new WaitForSeconds(1f);

        bluetooth.Send(Direction.Back);

        yield return new WaitForSeconds(1f);

        bluetooth.Send(Direction.Right);

        yield return new WaitForSeconds(1f);

        isTurning = false;

    }
   
    public void Back()
    {
        SceneManager.LoadScene("Connect");
        Log.Clear();
    }

}
