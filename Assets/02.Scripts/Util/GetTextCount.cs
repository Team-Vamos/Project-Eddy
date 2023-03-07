using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTextCount : MonoBehaviour
{
    [ContextMenu("텍스트 글자 수 가져오기")]
    private void GetTextCOunt()
    {
        Debug.Log(GetComponent<Text>().text.Length); 
    }
}