public interface IGrabbable
{
    public void ResetParent();

    public void AttachObject(UnityEngine.GameObject handOjb);

    public void DetatchObject(UnityEngine.Vector3 handObj);

    public void SetPhysicsActive(bool usePhysics);
}
