using UnityEngine;

namespace DungeonCrawl.Actors.Static.Environments
{
    public class RoadHorizontal : FloorType
    {
        void Start()
        {
            //Quaternion rotation = transform.localRotation;
            //rotation.z *= 270;
            //transform.localRotation = rotation;
            //transform.Rotate(Vector3.forward);
            //transform.Rotate(Vector3.right * 50 * Time.deltaTime, Space.World);
            //transform.Rotate(new Vector3(0, 0.3f, 0));
        }
        public override int DefaultSpriteId => 55;
        public override string DefaultName => "Horizontal Road";
    }
}
