using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IState
{
    public void Enter();

    public void Update();

    public void Exit();
}
