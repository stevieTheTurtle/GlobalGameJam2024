using System;
using UnityEngine;

public interface ICollectable
{
    public void CollectObjectFor(PlayerManager playerManager);
    public Transform GetTransform();
}
