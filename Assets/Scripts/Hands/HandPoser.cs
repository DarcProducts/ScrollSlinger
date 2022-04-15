using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

public class HandPoser : MonoBehaviour
{
    public Transform[] handBones;
    
    [Button]
    public void CreateNewPose()
    {
        var newPose = ScriptableObject.CreateInstance<Pose>();
        ProjectWindowUtil.CreateAsset(newPose, "New Pose.asset");
        newPose.bonePositions = new Vector3[handBones.Length];
        newPose.boneRotations = new Quaternion[handBones.Length];
        newPose.boneScales = new Vector3[handBones.Length];
        for (int i = 0; i < handBones.Length; i++)
        {
            newPose.bonePositions[i] = handBones[i].position;
            newPose.boneRotations[i] = handBones[i].rotation;
            newPose.boneScales[i] = handBones[i].localScale;
        }    
        AssetDatabase.SaveAssets();
    }
}
