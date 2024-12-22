using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Breakable
{
    //내구성 - 블럭의 HP, 애니메이션도 추가하면 좋을듯하다.
    private int durability = 1;
    public override void Break()
    {
        durability--;

        if (durability == 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
